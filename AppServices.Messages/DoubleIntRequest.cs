using Common.RequestProcessing.Messages;
using System.Runtime.Serialization;

namespace AppServices.Messages
{
    [DataContract]
    public class DoubleIntRequest : BaseRequest
    {
        [DataMember]
        public int Number { get; set; }

        public DoubleIntRequest()
        {
        }

        public DoubleIntRequest(
            string requestID,
            string requestingSystem,
            string requestingUser,
            int number) : base(requestID, requestingSystem, requestingUser)
        {
            this.Number = number;
        }
    }
}
