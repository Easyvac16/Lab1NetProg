using Core;
using System.Net;
using System.Net.Sockets;

namespace Lab1NetProg
{
    internal class client
    {
        private static IPAddress _address => IPAddress.Parse(Service.IpStr);
        static void Main()
        {
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);

            Console.WriteLine("Trying to connect to the server...");
            try
            {
                client.Connect(_address, Service.Port);

                if (!client.Connected)
                {
                    throw new Exception("Error connection");
                }

                Console.WriteLine(Service.RecieveMsg(client));

                Loop(client);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
            Console.WriteLine("Connection closed...");
        }

        public static bool ReadMsg(out string msg)
        {
            while (true)
            {
                Console.WriteLine("Write your message:");
                msg = Console.ReadLine() ?? "";

                if (msg.Length > 0)
                {
                    return msg != "exit";
                }
                Console.WriteLine("Message can't be empty");
            }


        }

        public static bool CheckAns(Socket client)
        {
            if (Service.RecieveMsg(client) == "t")
            {
                Console.WriteLine("Correct");
                return true;
            }
            Console.WriteLine("Inccorect");
            return false;
        }

        public static void Loop(Socket client)
        {
            while (true)
            {
                if (!ReadMsg(out string msg)) break;

                Service.SendMsg(client, msg);

                if (CheckAns(client)) break;

            }
        }
    }
}
