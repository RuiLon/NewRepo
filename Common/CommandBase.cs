using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Demo.Common
{
    public class CommandBase : ICommand
    {   
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if(DoCanExecute != null)
            {
                DoCanExecute.Invoke(parameter);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Execute(object parameter)
        {
            if(DoExecute != null) {
                DoExecute.Invoke(parameter);
            }
        }
           
        //事件执行对象
        public Action<object> DoExecute { get; set; }

        //时间是否可执行
        public Func<object,bool> DoCanExecute { get; set; }

        public CommandBase(Action<object> doExecute, Func<object, bool> doCanExecute)
        {
            DoExecute = doExecute;
            DoCanExecute = doCanExecute;
        }
        public CommandBase()
        {

        }
    }
}
