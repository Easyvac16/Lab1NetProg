using System.Net.Sockets;
using System.Net;
using System.Text;

namespace Lab1NetProg
{
    internal class client
    {
        private static IPAddress _address => IPAddress.Parse("127.0.0.1");
        private static int _port => 1024;
        static void Main()
        {
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);


            Console.WriteLine("Trying to connect to the server...");
            try
            {
                client.Connect(_address, _port);

                if (!client.Connected)
                {
                    throw new Exception("Error connection");
                }
                string msg = "Hello server!!!";

                SendMsg(client, msg);

                ShowLog(client);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

        }

        public static void SendMsg(Socket socket, string msg)
        {
            socket.Send(Encoding.UTF8.GetBytes(msg));
        }

        public static void ShowLog(Socket socket)
        {
            Console.WriteLine($"At {DateTime.Now} from {socket.RemoteEndPoint} recieved line: {RecieveMsg(socket)}");
        }

        public static string RecieveMsg(Socket socket)
        {
            var buffer = new byte[1024];

            int size = socket.Receive(buffer);

            return Encoding.UTF8.GetString(buffer, 0, size);
        }
    }
}
