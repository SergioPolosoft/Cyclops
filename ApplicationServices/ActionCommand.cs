using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationServices
{
    public class ActionCommand:ICommand
    {
        private Action Action { get; set; }

        public ActionCommand(Action action)
        {
            this.Action = action;
        }

        
        public void InvokeAction()
        {
            Action.Invoke();
        }
    }
}
