using System;

namespace QCRoutine.Application
{
    public class ApplicationNotExistsException:Exception
    {
        private int applicationTestCode;

        public ApplicationNotExistsException(int testCode)
        {
            applicationTestCode = testCode;
        }
    }
}