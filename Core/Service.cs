using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Core
{
    public class Service
    {
        public static string IpStr = "127.0.0.1";

        public static int Port => 1024;

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
