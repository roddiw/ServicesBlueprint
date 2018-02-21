using Common.AuditLogging;

namespace MathServices
{
    public class IntService : IIntService
	{
		public int AddInts(int number1, int number2)
        {
            var auditLog = Resolver.Get<IAuditLog>();
            var settings = Resolver.Get<ISettings>();

            int result = number1 + number2;

            auditLog.Info($"Added {number1} and {number2} to get result {result}");

            if (result > settings.AddIntsMaxResult)
            {
                result = settings.AddIntsMaxResult;
            }

            return result;
		}
	}
}
