using System;
using System.Runtime.Serialization;

namespace ClassLibraryMoq1
{
    [Serializable]
    public class CustomerCreationException : Exception
    {
        private InvalidCustomerMailingAddressException e;

        public CustomerCreationException()
        {
        }

        public CustomerCreationException(InvalidCustomerMailingAddressException e)
        {
            this.e = e;
        }

        public CustomerCreationException(string message) : base(message)
        {
        }

        public CustomerCreationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CustomerCreationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}