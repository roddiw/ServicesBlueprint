using Common.RequestProcessing.Messages;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AppServices.Messages
{
    public class DoubleIntResponse : BaseResponse
    {
        [DataMember]
        public int Result { get; set; }

        public DoubleIntResponse()
        {
        }

        public DoubleIntResponse(List<ResponseError> errors) : base(errors)
        {
            Result = 0;
        }

        public DoubleIntResponse(int result) : base()
        {
            this.Result = result;
        }
    }
}
