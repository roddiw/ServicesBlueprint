using System.ServiceModel;

namespace MathServices
{
    [ServiceContract]
	public interface IIntService
	{
		[OperationContract]
		int AddInts(int number1, int number2);
	}
}
