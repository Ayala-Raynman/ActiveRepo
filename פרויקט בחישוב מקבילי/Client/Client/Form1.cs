using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Form1 : Form
    {
        IPHostEntry host;
        IPAddress ipAddress;
        IPEndPoint remoteEP;
        Socket socket;
        //משתנה אסקי ששולח את הנתונים בסוקט
        byte[] bytes = new byte[1024];

        public void StartClient()
        {
            try
            {
                // Connect to a Remote server  
                // Get Host IP Address that is used to establish a connection  
                // In this case, we get one IP address of localhost that is IP : 127.0.0.1  
                // If a host has multiple addresses, you will get a list of addresses  
                host = Dns.GetHostEntry("localhost");
                ipAddress = host.AddressList[0];
                remoteEP = new IPEndPoint(ipAddress, 3000);

                // Create a TCP/IP  socket.    
                socket = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);

                // Connect the socket to the remote endpoint. Catch any errors.    
                try
                {
                    // Connect to Remote EndPoint  
                    socket.Connect(remoteEP);
                    Console.WriteLine("Socket connected to {0}",
                        socket.RemoteEndPoint.ToString());
                }
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }
                catch (SocketException se)
                {
                    Console.WriteLine("SocketException : {0}", se.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }




        public Form1()
        {
            InitializeComponent();
            StartClient();
        }


        private void delete_Click(object sender, EventArgs e)
        {
            byte[] msg = Encoding.UTF8.GetBytes("0");

            // Send the data through the socket.    
            int bytesSent = socket.Send(msg);

            // Receive the response from the remote device.    
            int bytesRec = socket.Receive(bytes);
            String s = Encoding.UTF8.GetString(bytes, 0, bytesRec);

            // Displays the MessageBox.
            MessageBox.Show(s);
            call.Enabled = true;
            call.BackColor = Color.FromName("Red");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
             socket.Shutdown(SocketShutdown.Both);
             socket.Close();
        }

        private void call_Click(object sender, EventArgs e)
        {
            byte[] msg = Encoding.UTF8.GetBytes("1");

            // Send the data through the socket.    
            int bytesSent = socket.Send(msg);

            // Receive the response from the remote device.    
            int bytesRec = socket.Receive(bytes);
            String s = Encoding.UTF8.GetString(bytes, 0, bytesRec);

            // Displays the MessageBox.
            MessageBox.Show(s);
            call.Enabled = false;
            call.BackColor = Color.FromName("LightGray");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
