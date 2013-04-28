using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZeroMQ;

namespace NServiceBus.Transports.ZeroMQ
{
	public class ZeroMqMessagePublisher : IPublishMessages, IDisposable
	{
		private int counter = 0;
		private ZmqSocket _pub;

		public ZeroMqMessagePublisher(ZmqContext context)
		{
			_pub = context.CreateSocket(SocketType.PUB);
			_pub.Bind("tcp://*:5555");

			Trace.WriteLine("Hello");
		}

		public bool Publish(TransportMessage message, IEnumerable<Type> eventTypes)
		{
			var status = _pub.Send(++counter + "", Encoding.Unicode);
			return (status == SendStatus.Sent);
		}

		public void Dispose()
		{
			_pub.Dispose();
		}
	}

	public class ZeroMqMessageSender : ISendMessages
	{
		public void Send(TransportMessage message, Address address)
		{
			
		}
	}
}