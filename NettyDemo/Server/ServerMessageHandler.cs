using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class ServerMessageHandler : ChannelHandlerAdapter
    {
        public override void ChannelRead(IChannelHandlerContext context, object message)
        {
            var buffer = message as IByteBuffer;
            if (buffer != null)
            {
                string receiveData = buffer.ToString(Encoding.UTF8);
                Console.WriteLine("服务端获取到:" + receiveData);
                byte[] messageBytes = Encoding.UTF8.GetBytes(receiveData);
                IByteBuffer byteBuffer = Unpooled.Buffer(256);
                byteBuffer.WriteBytes(messageBytes);
                context.WriteAndFlushAsync(byteBuffer);
            }
        }
        public override void ChannelReadComplete(IChannelHandlerContext context) => context.Flush();

        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception)
        {
            context.CloseAsync();
        }
    }
}
