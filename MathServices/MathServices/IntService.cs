using Common.RequestProcessing;
using MathServices.Messages;
using MathServices.Processing;

namespace MathServices
{
    public class IntService : IIntService
	{
		public AddIntsResponse AddInts(AddIntsRequest request)
        {
            return Resolver.Get<RequestProcessor>().Execute<AddIntsRequest, AddIntsResponse>(request, Resolver.Get<AddIntsAction>(), Resolver.Get<AddIntsRequestValidator>());
		}
	}
}
