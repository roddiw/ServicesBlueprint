using log4net;
using log4net.Config;
using System;
using System.ServiceModel;
using System.ServiceProcess;

namespace MathServices
{
    class Program
    {
        static void Main()
        {
            var serviceHostController = new ServiceHostController();

            if (!Environment.UserInteractive)
            {
                ServiceBase.Run(serviceHostController);
            }
            else
            {
                Console.WriteLine("Attempting to start service");
                serviceHostController.StartService();
                Console.WriteLine("Successfully started service\r\nPress Enter to exit");
                Console.ReadLine();
                Console.WriteLine("Attempting to stop service");
                serviceHostController.StopService();
                Console.WriteLine("Successfully stopped service");
            }
        }

        static void Main2(string[] args)
        {
            XmlConfigurator.Configure();
            ILog logger = LogManager.GetLogger("MathServices");

            try
            {
                //
                // start service
                //

                logger.Info("Attempting to start service");

                logger.Info("Attempting to initialize resolver");
                Resolver.Initialize(logger);
                logger.Info("Successfully initialized resolver");

                logger.Info("Attempting to open service host");
                var serviceHost = new ServiceHost(typeof(IntService));
                serviceHost.Open();
                logger.Info("Successfully opened service host");

                logger.Info("Successfully started service");

                Console.WriteLine("Press ENTER to exit.");
                Console.Read();

                //
                // stop service
                //

                logger.Info("Attempting to stop service");

                logger.Info("Attempting to close service host");
                serviceHost.Close();
                logger.Info("Successfully closed service host");

                logger.Info("Attempting to dispose resolver");
                Resolver.Dispose();
                logger.Info("Successfully disposed resolver");

                logger.Info("Successfully stopped service");
            }
            catch (Exception ex)
            {
                logger.Error("Unhandled exception", ex);
            }
        }
    }
}
