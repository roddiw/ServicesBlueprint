using log4net;

namespace Common.AuditLogging
{
    public class Log4NetAuditLog : IAuditLog
    {
        private readonly ILog logger;

        public Log4NetAuditLog(ILog logger)
        {
            this.logger = logger;
        }

        public void Info(string message)
        {
            logger.Info(message);
        }

        public void InfoFormat(string format, params object[] args)
        {
            logger.InfoFormat(format, args);
        }

        public void Warn(string message)
        {
            logger.Warn(message);
        }

        public void WarnFormat(string format, params object[] args)
        {
            logger.WarnFormat(format, args);
        }
    }
}
