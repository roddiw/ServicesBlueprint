using Common.RequestProcessing;
using MathService.Messages;
using MathServices.Processing;

namespace MathServices
{
    public class IntService : IIntService
	{
		public AddIntsResponse AddInts(AddIntsRequest request)
        {
            return Resolver.Get<RequestProcessor>().Execute<AddIntsRequest, AddIntsResponse>(request, Resolver.Get<AddIntsAction>());
		}
	}
}
