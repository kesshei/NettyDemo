using DotNetty.Buffers;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    /// <summary>
    /// 客户端
    /// </summary>
    public class NettyClinet    
    {
        public static IChannel Channel { set; get; }
        public static IEventLoopGroup Group { set; get; }
        public static ClientMessageHandler clientMessageHandler = new ClientMessageHandler();
        public async void Connect(string host, int port)
        {
            Group = new MultithreadEventLoopGroup();

            var bootstrap = new Bootstrap();
            bootstrap
                .Group(Group)
                .Channel<TcpSocketChannel>()
                .Option(ChannelOption.TcpNodelay, true)
                .Handler(new ActionChannelInitializer<ISocketChannel>(channel =>
                {
                    IChannelPipeline pipeline = channel.Pipeline;
                    pipeline.AddLast(clientMessageHandler);
                }));
            Channel = await bootstrap.ConnectAsync(new IPEndPoint(IPAddress.Parse(host), port));
        }
        public async void Close()
        {
            await Channel.CloseAsync();
            await Group.ShutdownGracefullyAsync(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1));
        }
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="message"></param>
        public bool SendMessage(string message)
        {
            if (message == null)
            {
                return false;
            }
            IByteBuffer buffer = Unpooled.Buffer(256);

            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            buffer.WriteBytes(messageBytes);
            Channel.WriteAndFlushAsync(buffer);

            return true;
        }
    }
}
