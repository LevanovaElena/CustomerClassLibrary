using System;
using System.Runtime.Serialization;

namespace CustomerClassLibrary.Common
{
    [Serializable]
    public class NotFoundAddressWithId : Exception
    {
        public NotFoundAddressWithId()
        {
        }

        public NotFoundAddressWithId(string message) : base(message)
        {
        }

        public NotFoundAddressWithId(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotFoundAddressWithId(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}