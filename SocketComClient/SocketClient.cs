using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SocketComClient
{
    internal class SocketClient
    {
        private static void Main(string[] args)
        {
            EndPoint _endPoint = new IPEndPoint(IPAddress.Loopback, 12345); //Same as server

            Console.WriteLine("Socket communication client!");

            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(_endPoint);

            UnicodeEncoding encoding = new UnicodeEncoding();

            char[] sendChars = "Hello friend!".ToCharArray();
            byte[] sendBytes = encoding.GetBytes(sendChars);

            byte[] receiveBytes = new byte[1024];

            Console.Write("Chars: ");
            for (int i = 0; i < sendChars.Length; i++)
            {
                Console.Write(sendChars[i]); 
            }
            Console.WriteLine();

            Console.WriteLine("Send bytes: ");
            for (int i = 0; i < sendBytes.Length; i++)
            {
                Console.Write(sendBytes[i]);
            }
            Console.WriteLine();

            int bytesReceived = 0;

            while (true) {

                Console.WriteLine("Sending data!");
                socket.Send(sendBytes);
                Thread.Sleep(1000);
                bytesReceived = socket.Receive(receiveBytes);

                Console.WriteLine("Receiving data! Amount: " + bytesReceived);
                Console.WriteLine(encoding.GetString(receiveBytes));

                Thread.Sleep(500);
            }
        }
    }
}
