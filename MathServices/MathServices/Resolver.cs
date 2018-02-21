﻿using Common.AuditLogging;
using log4net;
using Ninject;

namespace MathServices
{
    public static class Resolver
    {
        private static StandardKernel kernel;
        private static ILog logger;

        public static void Initialize(ILog logger)
        {
            Resolver.logger = logger;
            Resolver.kernel = GetKernel();
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

            kernel.Bind<IAuditLog>().ToConstant(new Log4NetAuditLog(LogManager.GetLogger("Audit")));
            kernel.Bind<ISettings>().ToConstant(new ConfigFileSettings());

            return kernel;
        }
    }
}