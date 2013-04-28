namespace NServiceBus.Transports.ZeroMQ.Config
{
	public class ZeroMqTransportConfigurer : ConfigureTransport<ZeroMQ>
	{
		protected override string ExampleConnectionStringForErrorMessage { get { return "tcp://*:5555"; } }

		protected override void InternalConfigure(Configure config, string connectionString)
		{
			config.Configurer.ConfigureComponent<ZeroMqMessagePublisher>(DependencyLifecycle.InstancePerCall);
		}
	}
}