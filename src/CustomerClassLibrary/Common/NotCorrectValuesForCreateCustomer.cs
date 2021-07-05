using System;
using System.Runtime.Serialization;

namespace CustomerClassLibrary.Common
{
    [Serializable]
    internal class NotCorrectValuesForCreateCustomer : Exception
    {
        public NotCorrectValuesForCreateCustomer()
        {
        }

        public NotCorrectValuesForCreateCustomer(string message) : base(message)
        {
        }

        public NotCorrectValuesForCreateCustomer(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotCorrectValuesForCreateCustomer(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}