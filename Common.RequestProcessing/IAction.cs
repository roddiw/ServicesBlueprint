using Common.RequestProcessing.Messages;

namespace Common.RequestProcessing
{
    public interface IAction<in TRequest, out TResponse>
        where TRequest : BaseRequest
        where TResponse : BaseResponse, new()
    {
        TResponse Execute(TRequest request);
    }
}
