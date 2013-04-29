using NServiceBus.Installation.Environments;

namespace NServiceBus.Transports.ZeroMQ.SampleHost
{
    using NServiceBus;
	using Config;
	/*
		This class configures this endpoint as a Server. More information about how to configure the NServiceBus host
		can be found here: http://nservicebus.com/GenericHost.aspx
	*/
	public class EndpointConfig : IConfigureThisEndpoint, AsA_Server, UsingTransport<ZeroMQ>, IWantCustomInitialization
    {
		public void Init()
		{
			Configure.With(AllAssemblies.Except("libzmq-x64-3.0.0.0.dll"));
			Installer<Windows>.RunOtherInstallers = false;
		}
    }
}