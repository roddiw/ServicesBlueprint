using Common.RequestProcessing;
using MathService.Messages;

namespace MathServices.Processing
{
    class AddIntsAction : IAction<AddIntsRequest, AddIntsResponse>
    {
        private readonly ISettings settings;

        public AddIntsAction(ISettings settings)
        {
            this.settings = settings;
        }

        public AddIntsResponse Execute(AddIntsRequest request)
        {
            int result = request.Number1 + request.Number2;

            if (result > settings.AddIntsMaxResult)
            {
                result = settings.AddIntsMaxResult;
            }

            return new AddIntsResponse(result);
        }
    }
}
