using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ServerSide
{
    class HandleClient
    {
        Socket clientSocket;
        string clNo;
        public void startClient(Socket inClientSocket, string clineNo)
        {
            this.clientSocket = inClientSocket;
            this.clNo = clineNo;
            Thread ctThread = new Thread(go);
            ctThread.Start();
        }
        private void go()
        {
            // Incoming data from the client.    
            string data = null;
            byte[] bytes = null;
            bytes = new byte[1024];

            try
            {
                while (true)
                {
                    bytes.Clone();
                    int bytesRec = clientSocket.Receive(bytes);
                    data = Encoding.UTF8.GetString(bytes, 0, bytesRec);
                    if (data.IndexOf("<EOF>") > -1)
                    {
                        break;
                    }
                    switch (data)
                    {
                        case "1":
                            NurseQueue.Add(clientSocket);
                            break;
                        case "0":
                            NurseQueue.Delete(clientSocket);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(" >> " + ex.ToString());
            }
            finally
            {
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();
            }  
        }
    }
}
