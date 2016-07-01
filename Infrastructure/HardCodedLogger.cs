using System;
using ApplicationServices;

namespace Infrastructure
{
    public class HardCodedLogger : ILogger
    {
        public void Warning(Exception exception)
        {
            Console.WriteLine("WARN: An exception occurred. {0}", exception.Message);
        }
    }
}