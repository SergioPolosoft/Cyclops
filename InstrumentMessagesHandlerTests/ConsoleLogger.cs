using System;
using ApplicationServices;

namespace InstrumentMessagesHandlerTests
{
    public class ConsoleLogger : ILogger
    {
        public void Warning(Exception exception)
        {
            Console.WriteLine("WARN: An exception occurred. {0}", exception.Message);
        }
    }
}