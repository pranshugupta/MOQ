using System;
using System.Runtime.Serialization;

namespace ClassLibraryMoq1
{
    [Serializable]
    internal class InValidWorkStationException : Exception
    {
        public InValidWorkStationException()
        {
        }

        public InValidWorkStationException(string message) : base(message)
        {
        }

        public InValidWorkStationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InValidWorkStationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}