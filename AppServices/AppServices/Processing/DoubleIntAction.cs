using AppServices.Messages;
using Common.RequestProcessing;
using Common.RequestProcessing.Messages;
using MathServices.Messages;
using System.Linq;

namespace AppServices.Processing
{
    class DoubleIntAction : IAction<DoubleIntRequest, DoubleIntResponse>
    {
        public DoubleIntResponse Execute(DoubleIntRequest request)
        {
            AddIntsResponse addIntsResponse;
            var client = new IntService.IntServiceClient();
            try
            {
                // we can use AddIntsRequest constructor because we referenced MathServices.Messages project
                addIntsResponse = client.AddInts(new AddIntsRequest(request.RequestID, request.RequestingSystem, request.RequestingSystem, request.Number, request.Number));
                client.Close();
            }
            catch
            {
                client.Abort();
                throw;
            }

            if (addIntsResponse.Errors.Any())
            {
                // normally IntService.AddInts errors would not be returned directly as IntService is not visible to the DoubleService client.
                // instead errors meaningful to the DoubleService.DoubleInt client should be returned.
                return new DoubleIntResponse(new ResponseError(ErrorCode.ExecutionError, "", "Could not perform operation").ToList());
            }

            return new DoubleIntResponse(addIntsResponse.Result);
        }
    }
}
