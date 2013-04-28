using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZeroMQ;

namespace NServiceBus.Transports.ZeroMQ
{
	public class ZeroMqMessagePublisher : IPublishMessages
	{
		public ZeroMqMessagePublisher()
		{
			Task.Factory.StartNew(Publish);
		}

		public bool Publish(TransportMessage message, IEnumerable<Type> eventTypes)
		{
			return true;
		}

		public void Publish()
		{
			using (ZmqContext context = ZmqContext.Create())
			using (ZmqSocket server = context.CreateSocket(SocketType.PUB))
			{
				server.Bind("tcp://*:5555");
				var counter = 0;
				while (true)
				{
					// Do Some 'work'
					Thread.Sleep(1000);

					// Send reply back to client
					server.Send(++counter + "", Encoding.Unicode);
				}
			}
		}
	}
}