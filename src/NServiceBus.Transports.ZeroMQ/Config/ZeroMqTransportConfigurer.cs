using NServiceBus.Unicast.Queuing.Installers;
using ZeroMQ;

namespace NServiceBus.Transports.ZeroMQ.Config
{
	public class ZeroMqTransportConfigurer : ConfigureTransport<ZeroMQ>
	{
		protected override string ExampleConnectionStringForErrorMessage { get { return "publishPort=5555;receivePort=5556"; } }

		protected override void InternalConfigure(Configure config, string connectionString)
		{
			config.Configurer.ConfigureComponent<ZmqContext>(builder => ZmqContext.Create(), DependencyLifecycle.SingleInstance);
			config.Configurer.ConfigureComponent<ZeroMqMessageDequeueStrategy>(DependencyLifecycle.InstancePerCall);
			config.Configurer.ConfigureComponent<ZeroMqMessageSender>(DependencyLifecycle.InstancePerCall); 
			config.Configurer.ConfigureComponent<ZeroMqMessagePublisher>(DependencyLifecycle.InstancePerCall);
			config.Configurer.ConfigureComponent<ZeroMqQueueCreator>(DependencyLifecycle.InstancePerCall);

			EndpointInputQueueCreator.Enabled = true;
		}
	}
}