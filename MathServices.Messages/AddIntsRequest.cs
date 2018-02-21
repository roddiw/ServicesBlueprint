using Common.RequestProcessing.Messages;
using System.Runtime.Serialization;

namespace MathService.Messages
{
    [DataContract]
    public class AddIntsRequest : BaseRequest
    {
        [DataMember]
        public int Number1 { get; set; }

        [DataMember]
        public int Number2 { get; set; }

        public AddIntsRequest()
        {
        }

        public AddIntsRequest(
            string requestID,
            string requestingSystem,
            string requestingUser,
            int number1,
            int number2) : base(requestID, requestingSystem, requestingUser)
        {
            this.Number1 = number1;
            this.Number2 = number2;
        }
    }
}
