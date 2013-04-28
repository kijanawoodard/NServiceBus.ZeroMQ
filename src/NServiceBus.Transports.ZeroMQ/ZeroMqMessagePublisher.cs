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
	public class ZeroMqMessagePublisher : IPublishMessages
	{
		private readonly ZmqContext _context;
		private int counter = 0;
		private ZmqSocket _request;
		private ZmqSocket _control;

		public ZeroMqMessagePublisher(ZmqContext context)
		{
			_context = context;
			_control = _context.CreateSocket(SocketType.REP);
			_request = _context.CreateSocket(SocketType.REQ);

			_control.Bind("inproc://publish");
			_request.Connect("inproc://publish");

			Task.Factory.StartNew(Publish);
		}

		public bool Publish(TransportMessage message, IEnumerable<Type> eventTypes)
		{
			var status = _request.Send(++counter + "", Encoding.Unicode);
			_request.Receive(Encoding.Unicode);
			return (status == SendStatus.Sent);
		}

		private void Publish()
		{
			Trace.WriteLine("Hello");
			using (var pub = _context.CreateSocket(SocketType.PUB))
			{
				pub.Bind("tcp://*:5555");
				
				while (true)
				{
					var msg = _control.Receive(Encoding.Unicode, TimeSpan.FromMilliseconds(200));
					
					if (_control.ReceiveStatus == ReceiveStatus.Interrupted) break;
					if (msg == null) continue;
					
					var status = pub.Send(msg, Encoding.Unicode);
					_control.Send("ok", Encoding.Unicode);
					if (status == SendStatus.Interrupted)
						break;
				}
			}

			Trace.WriteLine("Publisher Done");
		}
	}

	public class ZeroMqMessageSender : ISendMessages
	{
		public void Send(TransportMessage message, Address address)
		{
			
		}
	}
}