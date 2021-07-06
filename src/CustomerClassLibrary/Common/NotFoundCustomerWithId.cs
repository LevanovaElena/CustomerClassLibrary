using System;
using System.Runtime.Serialization;

namespace CustomerClassLibrary.Common
{
    [Serializable]
    public class NotFoundCustomerWithId : Exception
    {
        public NotFoundCustomerWithId()
        {
        }

        public NotFoundCustomerWithId(string message) : base(message)
        {
        }

        public NotFoundCustomerWithId(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotFoundCustomerWithId(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}