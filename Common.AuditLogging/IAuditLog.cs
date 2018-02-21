namespace Common.AuditLogging
{
    public interface IAuditLog
    {
        void Info(string message);
        void InfoFormat(string format, params object[] args);
        void Warn(string message);
        void WarnFormat(string format, params object[] args);
    }
}
