using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLTT.Command
{
    public class CommandInvoker
    {
        private ICommand _command;

        public void SetCommand(ICommand command)
        {
            _command = command;
        }

        public void Invoke()
        {
            if (_command != null)
            {
                _command.Execute();
            }
            else
            {
               
                throw new InvalidOperationException("Không có lệnh nào được đặt.");
            }
        }
    }

}