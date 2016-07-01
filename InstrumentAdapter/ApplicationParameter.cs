using System;

namespace InstrumentCommunication.Application
{
    public class ApplicationParameter
    {
        public ApplicationParameter()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; private set; }
    }
}