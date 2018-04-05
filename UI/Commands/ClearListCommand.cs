using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace UI.Commands
{
    public class ClearListCommand : ICommand
    {
        ViewModels.LiveRateViewModel LRVM;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public ClearListCommand(ViewModels.LiveRateViewModel vm)
        {
            this.LRVM = vm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            LRVM.ClearListView();
        }
    }
}
