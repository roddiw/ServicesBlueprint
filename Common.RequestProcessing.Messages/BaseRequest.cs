using System.Runtime.Serialization;

namespace Common.RequestProcessing.Messages
{
    // TODO property validation
    [DataContract]
    public class BaseRequest
    {
        /// <summary>
        /// Mandatory. NonWhiteSpaceString(1, 100). 
        /// </summary>
        [DataMember]
        public string RequestID { get; set; }

        /// <summary>
        /// Mandatory. NonWhiteSpaceString(1, 100). 
        /// </summary>
        [DataMember]
        public string RequestingSystem { get; set; }

        /// <summary>
        /// Mandatory. NonWhiteSpaceString(1, 100). 
        /// </summary>
        [DataMember]
        public string RequestingUser { get; set; }

        public BaseRequest()
        {
        }

        public BaseRequest(
            string requestID,
            string requestingSystem,
            string requestingUser)
        {
            this.RequestID = requestID;
            this.RequestingSystem = requestingSystem;
            this.RequestingUser = requestingUser;
        }

        public virtual BaseRequest ToLoggableRequest()
        {
            return this;
        }
    }
}