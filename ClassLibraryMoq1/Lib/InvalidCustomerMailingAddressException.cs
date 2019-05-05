using System;
using System.Runtime.Serialization;

namespace ClassLibraryMoq1
{
    [Serializable]
    public class InvalidCustomerMailingAddressException : Exception
    {
        public InvalidCustomerMailingAddressException()
        {
        }

        public InvalidCustomerMailingAddressException(string message) : base(message)
        {
        }

        public InvalidCustomerMailingAddressException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidCustomerMailingAddressException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}