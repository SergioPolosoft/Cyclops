using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationServices
{
    public interface IResponse
    {
        CommandResult Status { get; }
        string Message { get; }
    }
}
