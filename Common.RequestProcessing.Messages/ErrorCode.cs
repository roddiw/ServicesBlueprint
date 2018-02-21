using System.Runtime.Serialization;

namespace Common.RequestProcessing.Messages
{
    [DataContract]
    public enum ErrorCode
    {
        /// <summary>
        /// An execution failure that can be expected, eg if request field values are incorrect.
        /// </summary>
        [EnumMember]
        ExecutionError,

        /// <summary>
        /// An execution failure that can not be expected, eg network failure.
        /// </summary>
        [EnumMember]
        ExecutionException,

        /// <summary>
        /// A request validation error.
        /// </summary>
        [EnumMember]
        ValidationError
    }
}