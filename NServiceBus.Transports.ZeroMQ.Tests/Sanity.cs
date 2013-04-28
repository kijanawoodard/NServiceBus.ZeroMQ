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
			var context = ZmqContext.Create();
			var sut = new ZeroMqMessagePublisher(context);
			Task.Factory.StartNew(Subscribe);
			
			for (var i = 0; i < 5; i++)
			{
				Thread.Sleep(1000); 
				sut.Publish(null, null);
			}

			sut.Dispose(); 
			context.Dispose();
		}

		private void Subscribe()
		{
			Trace.WriteLine("World");
			using (var context = ZmqContext.Create())
			using (var socket = context.CreateSocket(SocketType.SUB))
			{
				socket.SubscribeAll();
				socket.Connect("tcp://127.0.0.1:5555");

				while (true)
				{
					var msg = socket.Receive(Encoding.Unicode);
					Trace.WriteLine("Received: " + msg);
				}
			}
		}

	}
}
