using System;

namespace QCConfiguration.Application
{
    public class UnknownApplicationException:Exception
    {
        public UnknownApplicationException(string message):base(message)
        {
        }
    }
}