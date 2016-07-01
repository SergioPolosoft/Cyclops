using System;

namespace ApplicationServices
{
    public interface ILogger
    {
        void Warning(Exception exception);
    }
}