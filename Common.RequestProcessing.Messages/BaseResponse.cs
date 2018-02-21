using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Common.RequestProcessing.Messages
{
    [DataContract]
    public class BaseResponse
    {
        [DataMember]
        public string RequestID { get; set; }

        [DataMember]
        public List<ResponseError> Errors { get; set; }

        public BaseResponse()
        {
            Errors = new List<ResponseError>();
        }

        public BaseResponse(List<ResponseError> errors)
        {
            this.Errors = errors;
        }

        public virtual BaseResponse ToLoggableResponse()
        {
            return this;
        }
    }
}