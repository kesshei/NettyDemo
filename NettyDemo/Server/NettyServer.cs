using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    /// <summary>
    /// 服务
    /// </summary>
    public class NettyServer
    {
        public async Task Listen(int port)
        {
            var boos = new MultithreadEventLoopGroup(1);
            var work = new MultithreadEventLoopGroup();

            var bootstrap = new ServerBootstrap();
            bootstrap.Group(boos, work)
                .Channel<TcpServerSocketChannel>()
                .Option(ChannelOption.SoBacklog, 100)
                .ChildHandler(new ActionChannelInitializer<ISocketChannel>(channel => {
                    IChannelPipeline pipeline = channel.Pipeline;

                    pipeline.AddLast(new ServerMessageHandler());
                }));
            IChannel boundChannel = await bootstrap.BindAsync(port);
        }
    }
}
