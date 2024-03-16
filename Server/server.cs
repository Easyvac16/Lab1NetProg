using System.Net.Sockets;
using System.Net;
using System.Text;

namespace Server
{
    internal class server
    {
        private static IPAddress _address => IPAddress.Parse("127.0.0.1");
        private static int _port => 1024;
        static void Main()
        {
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);

            server.Bind(new IPEndPoint(_address, _port));

            server.Listen();

            Socket client;

            try
            {
                while (true)
                {
                    client = server.Accept();

                    ShowLog(client);

                    SendMsg(client, "Hello client!!!");

                }
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
