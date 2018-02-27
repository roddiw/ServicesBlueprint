using AppServices.Messages;
using AppServices.Processing;
using Common.RequestProcessing;

namespace AppServices
{
    public class AppService : IAppService
    {
        public DoubleIntResponse DoubleInt(DoubleIntRequest request)
        {
            return Resolver.Get<RequestProcessor>().Execute<DoubleIntRequest, DoubleIntResponse>(request, Resolver.Get<DoubleIntAction>());
        }
    }
}
