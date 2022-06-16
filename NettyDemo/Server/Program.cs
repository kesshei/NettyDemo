using System;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Netty Server Test  by 蓝创精英团队";
            var server = new NettyServer();
            server.Listen(9999).Wait();
            Console.WriteLine("服务启动");
            Console.ReadLine();
        }
    }
}
