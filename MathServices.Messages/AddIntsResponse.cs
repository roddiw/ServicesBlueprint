using Common.RequestProcessing.Messages;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MathServices.Messages
{
    public class AddIntsResponse : BaseResponse
    {
        [DataMember]
        public int Result { get; set; }

        public AddIntsResponse()
        {
        }

        public AddIntsResponse(List<ResponseError> errors) : base(errors)
        {
            Result = 0;
        }

        public AddIntsResponse(int result) : base()
        {
            this.Result = result;
        }
    }
}
