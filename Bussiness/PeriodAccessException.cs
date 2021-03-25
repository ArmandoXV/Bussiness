using System;
using System.Runtime.Serialization;

namespace Bussiness
{
    [Serializable]
    internal class PeriodAccessException : Exception
    {
        public PeriodAccessException()
        {
        }

        public PeriodAccessException(string message) : base(message)
        {
        }

        public PeriodAccessException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PeriodAccessException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}