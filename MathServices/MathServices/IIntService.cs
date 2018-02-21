using MathServices.Messages;
using System.ServiceModel;

namespace MathServices
{
    [ServiceContract]
	public interface IIntService
	{
		[OperationContract]
        AddIntsResponse AddInts(AddIntsRequest request);
    }
}
