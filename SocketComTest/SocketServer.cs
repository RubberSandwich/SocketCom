using System.Net.Sockets;
using System.Net;
using System.Numerics;
using System.Text;

namespace SocketComTest
{
    internal class SocketServer
    {
        static int totalBytesRead = 0;
        static int totalBytesWrite = 0;
        static Socket? socket;
        static Socket? state;
        public static void Main(string[] args)
        {
            EndPoint _socketEndPoint = new IPEndPoint(IPAddress.Any, 12345);

            Console.WriteLine("Socket communication test program!");

            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            socket.Bind(_socketEndPoint);
            socket.Listen();
            state = socket.Accept();

            byte[] recvBuffer = new byte[512];
            byte[] bytes = Encoding.Unicode.GetBytes("Hello buddy!");

            for (int i = 0; i < bytes.Length; i++)
            {
                Console.Write(bytes[i]);
            }
            Console.WriteLine();

            while (true)
            {
                try { 
                    int recvCount = state.Receive(recvBuffer);
                    Console.WriteLine("Reading bytes!");
                    totalBytesRead += recvCount;

                    Thread.Sleep(100);

                    state.Send(bytes);
                    Console.WriteLine("Sending bytes!");

                    totalBytesWrite += bytes.Length;
                }catch (SocketException se) { 
                    state.Disconnect(true);
                    Thread.Sleep(500);
                }
                catch (Exception e) { 
                    //pass
                }
            }
        }

        private static void printSocketInfo(Socket socket)
        {
            Console.WriteLine("---------------Socket Info---------------");
            Console.WriteLine("Local endpoint: " + socket.LocalEndPoint);
            Console.WriteLine("Remote endpoint: " + socket.RemoteEndPoint);
            Console.WriteLine("Protocol type: " + socket.ProtocolType);
            Console.WriteLine("Connected (since last communication): " + socket.Connected);
        }
    }
}