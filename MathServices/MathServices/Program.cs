using System;
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
    }
}
