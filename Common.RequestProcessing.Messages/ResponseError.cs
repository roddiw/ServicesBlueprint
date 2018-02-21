using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Common.RequestProcessing.Messages
{
    [DataContract]
    public class ResponseError
    {
        [DataMember]
        public ErrorCode Code { get; set; }

        [DataMember]
        public string Subcode { get; set; }

        [DataMember]
        public string Description { get; set; }

        public ResponseError()
        {
        }

        public ResponseError(ErrorCode code)
            : this(code: code, subcode: null, description: code.ToString())
        {
        }

        public ResponseError(
            ErrorCode code,
            string description
        )
            : this(code: code, subcode: null, description: description)
        {
        }

        public ResponseError(
            ErrorCode code,
            string subcode,
            string description
        )
        {
            this.Code = code;
            this.Subcode = subcode;
            this.Description = description;
        }

        public List<ResponseError> ToList()
        {
            return new List<ResponseError> { this };
        }
    }
}