using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UI.ViewModels;

namespace UI.Commands
{
    class ExitProgramCommad : ICommand
    {
        MainWindowVM conVM;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public ExitProgramCommad(ViewModels.MainWindowVM vm)
        {
            this.conVM = vm;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
           // conVM();
        }
    }
}
