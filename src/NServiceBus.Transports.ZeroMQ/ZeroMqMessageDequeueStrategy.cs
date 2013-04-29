using System;
using NServiceBus.Unicast.Transport;

namespace NServiceBus.Transports.ZeroMQ
{
	public class ZeroMqMessageDequeueStrategy : IDequeueMessages
	{
		public void Init(Address address, TransactionSettings transactionSettings, Func<TransportMessage, bool> tryProcessMessage, Action<string, Exception> endProcessMessage)
		{
			
		}

		public void Start(int maximumConcurrencyLevel)
		{
			
		}

		public void Stop()
		{
			
		}
	}
}