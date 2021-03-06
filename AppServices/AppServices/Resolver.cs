﻿using Common.AuditLogging;
using Common.RequestProcessing;
using log4net;
using Ninject;

namespace AppServices
{
    public static class Resolver
    {
        private static StandardKernel kernel;
        private static ILog logger;

        public static void Initialize(
            ILog logger,
            StandardKernel customKernel = null)
        {
            Resolver.logger = logger;
            Resolver.kernel = customKernel ?? GetKernel();
        }

        public static void Dispose()
        {
            kernel.Dispose();
        }

        public static T Get<T>()
        {
            return kernel.Get<T>();
        }

        private static StandardKernel GetKernel()
        {
            var kernel = new StandardKernel();

            kernel.Bind<ILog>().ToConstant(logger);

            IAuditLog auditLogger = new Log4NetAuditLog(LogManager.GetLogger("Audit"));
            kernel.Bind<IAuditLog>().ToConstant(auditLogger);

            kernel.Bind<RequestProcessor>().ToConstant(new RequestProcessor(logger, auditLogger));

            return kernel;
        }
    }
}
