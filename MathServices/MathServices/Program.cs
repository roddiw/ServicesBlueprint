using Common.AuditLogging;
using log4net;
using log4net.Config;
using System;

namespace MathServices
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlConfigurator.Configure();
            ILog logger = LogManager.GetLogger("MathServices");

            try
            {
                logger.Info("Service started");

                IAuditLog auditLogger = new Log4NetAuditLog(LogManager.GetLogger("Audit"));
                auditLogger.Info("Test audit log message");

                Console.WriteLine("Service started. Press ENTER to exit.");
                Console.Read();

                logger.Info("Service stopped");
            }
            catch (Exception ex)
            {
                logger.Error("Unhandled exception", ex);
            }
        }
    }
}
