using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Revit_WPF_Example.Commands
{
  public  class CustomCommand: ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public Action CommandAction = () => { };
        public void Execute(object parameter)
        {
                
               CommandAction();
        }
    }
}
