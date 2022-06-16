using System;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Netty Clinet Test  by 蓝创精英团队";
            var clinet = new NettyClinet();
            clinet.Connect("127.0.0.1", 9999);
            Console.WriteLine("客户端开始连接!");
            string content = "";
            while (!(content = Console.ReadLine()).Contains("Exit", StringComparison.InvariantCultureIgnoreCase))
            {
                clinet.SendMessage(content);
            }
            Console.ReadLine();
        }
    }
}
