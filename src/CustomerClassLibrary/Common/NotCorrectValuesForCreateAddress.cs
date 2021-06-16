using System;
using System.Runtime.Serialization;

namespace CustomerClassLibrary
{
    [Serializable]
    internal class NotCorrectValuesForCreateAddress : Exception
    {
        public NotCorrectValuesForCreateAddress()
        {
        }

        public NotCorrectValuesForCreateAddress(string message) : base(message)
        {
        }

        public NotCorrectValuesForCreateAddress(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotCorrectValuesForCreateAddress(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}