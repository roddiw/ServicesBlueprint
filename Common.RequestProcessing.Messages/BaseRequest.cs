using System.Runtime.Serialization;

namespace Common.RequestProcessing.Messages
{
    [DataContract]
    public class BaseRequest
    {
        /// <summary>
        /// Mandatory. Non empty and 100 characters or less. 
        /// </summary>
        [DataMember]
        public string RequestID { get; set; }

        /// <summary>
        /// Mandatory. Non empty and 100 characters or less. 
        /// </summary>
        [DataMember]
        public string RequestingSystem { get; set; }

        /// <summary>
        /// Mandatory. Non empty and 100 characters or less. 
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