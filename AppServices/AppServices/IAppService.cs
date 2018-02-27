using AppServices.Messages;
using System.ServiceModel;

namespace AppServices
{
    [ServiceContract]
    public interface IAppService
    {
        [OperationContract]
        DoubleIntResponse DoubleInt(DoubleIntRequest request);
    }
}
