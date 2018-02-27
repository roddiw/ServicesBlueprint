using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceProcess;

namespace MathServices
{
    public partial class ServiceHostController : ServiceBase
    {
        private static ILog logger;
        private static List<ServiceHost> serviceHosts;

        public ServiceHostController()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            StartService();
        }

        protected override void OnStop()
        {
            StopService();
        }

        public void StartService()
        {
            XmlConfigurator.Configure();
            logger = LogManager.GetLogger("MathServices");

            try
            {
                logger.Warn("Attempting to start service");

                // first create dependency resolver
                logger.Info("Attempting to initialize resolver");
                Resolver.Initialize(logger);
                logger.Info("Successfully initialized resolver");

                // lastly create service hosts
                logger.Info("Attempting to create service hosts");
                serviceHosts = new List<ServiceHost> {
                    CreateServiceHost<IntService>() };
                logger.Info("Successfully created service hosts");

                logger.Warn("Successfully started service");
            }
            catch (Exception ex)
            {
                logger.Error("Failed to start service", ex);
                throw; // rethrow exception so that service startup aborts
            }
        }

        public void StopService()
        {
            try
            {
                logger.Warn("Attempting to stop service");

                // close service hosts
                logger.Info("Attempting to close service hosts");
                foreach (ServiceHost serviceHost in serviceHosts)
                {
                    try
                    {
                        serviceHost.Close();
                    }
                    catch (Exception ex)
                    {
                        logger.Error("Exception closing service hosts", ex);
                    }
                }
                logger.Info("Successfully closed service hosts");

                logger.Info("Attempting to dispose resolver");
                Resolver.Dispose();
                logger.Info("Successfully disposed resolver");

                logger.Warn("Successfully stopped service");
            }
            catch (Exception ex)
            {
                logger.Error("Failed to stop service", ex);
            }
        }

        private ServiceHost CreateServiceHost<ServiceType>()
        {
            ServiceHost serviceHost = new ServiceHost(typeof(ServiceType));
            serviceHost.Faulted += ServiceHostOnFaulted<ServiceType>;
            serviceHost.Open();
            return serviceHost;
        }

        private void ServiceHostOnFaulted<ServiceType>(
            object eventSender,
            EventArgs eventArgs)
        {
            try
            {
                ServiceHost serviceHost = eventSender as ServiceHost;
                if (serviceHost == null)
                {
                    throw new Exception("Could not get serviceHost from eventSender");
                }
                serviceHosts.Remove(serviceHost);

                Type serviceType = typeof(ServiceType);
                logger.Error(serviceType.Name + " faulted. Attempting to restart.");
                serviceHost.Abort();
                serviceHost = new ServiceHost(serviceType);
                serviceHost.Faulted += new EventHandler(ServiceHostOnFaulted<ServiceType>);
                serviceHost.Open();
                logger.Error("successfully restarted " + serviceType.Name);

                serviceHosts.Add(serviceHost);
            }
            catch (Exception ex)
            {
                try
                {
                    Type serviceType = typeof(ServiceType);
                    logger.Error("Exception restarting " + serviceType.Name, ex);
                }
                catch { }
            }
        }
    }
}
