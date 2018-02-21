using Common.RequestProcessing;
using MathService.Messages;
using System;

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
                throw new Exception("result is too large");
            }

            return new AddIntsResponse(result);
        }
    }
}
