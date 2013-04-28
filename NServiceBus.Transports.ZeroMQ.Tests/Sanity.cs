using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NServiceBus.Transports.ZeroMQ.Config;
using NUnit.Framework;
using ZeroMQ;

namespace NServiceBus.Transports.ZeroMQ.Tests
{
	[TestFixture]
    public class Sanity
    {
		[SetUp]
		public void Setup()
		{
			//Address.InitializeLocalAddress("tcp://*:5555");
		}

		[Test]
		public void CanSub()
		{
			var sut = new ZeroMqMessagePublisher();
			Task.Factory.StartNew(Subscribe);
			Thread.Sleep(11000);
			
		}

		private void Subscribe()
		{
			using (var ctx = ZmqContext.Create())
			using (var socket = ctx.CreateSocket(SocketType.SUB))
			{
				socket.SubscribeAll();
				socket.Connect("tcp://127.0.0.1:5555");

				while (true)
				{
					var msg = socket.Receive(Encoding.Unicode);
					Trace.WriteLine("Received: " + msg);
					Thread.Sleep(500);
					
				}
			}
		}

	}
}
