using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace ServerSide
{
    static class NurseQueue
    {
        static LinkedList<Socket> list = new LinkedList<Socket>();//רשימת ממתינים לאחות
        static bool nurse = false;

        public static void Add(Socket s)//הוספת ממתין לתור
        {
            lock ("key")
            {
                byte[] msg;
                list.AddLast(s);
                if (nurse == false && list.First.Value == s)
                {
                    nurse = true;
                    msg = Encoding.UTF8.GetBytes("האחות לשרותך");
                }
                else
                    msg = Encoding.UTF8.GetBytes("האחות עסוקה כרגע. המתן בסבלנות");

                // Send the data through the socket.    
                int bytesSent = s.Send(msg);
            }
        }
        public static void Delete(Socket s)//סיום טיפול במטופל ושחרור האחות לצטופל הבא
        {
            lock ("key")
            {
                byte[] msg;
                if (list.Find(s) != null)
                {
                    if (list.First.Value == s) { nurse = false;  }
                    list.Remove(s);
                    msg = Encoding.UTF8.GetBytes("שמחנו לעזור לך");
                }
                else msg = Encoding.UTF8.GetBytes("שגיאה. אינך נמצא בתור");

                // Send the data through the socket.    
                int bytesSent = s.Send(msg);
            }
        }
    }
}
