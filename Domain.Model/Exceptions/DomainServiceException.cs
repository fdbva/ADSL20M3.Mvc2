using System;

namespace Domain.Model.Exceptions
{
    public class DomainServiceException : ApplicationException
    {
        public DomainServiceException(
            string message)
            : base(message)
        {

        }

        public DomainServiceException(
            string message,
            Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
