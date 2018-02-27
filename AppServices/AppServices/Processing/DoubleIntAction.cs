using AppServices.Messages;
using Common.RequestProcessing;

namespace AppServices.Processing
{
    class DoubleIntAction : IAction<DoubleIntRequest, DoubleIntResponse>
    {
        public DoubleIntResponse Execute(DoubleIntRequest request)
        {
            return new DoubleIntResponse(request.Number + request.Number);
        }
    }
}
