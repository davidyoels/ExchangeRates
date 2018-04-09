using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace UI.Commands
{
    public class MenuLiveRateCommand : ICommand
    {
        ViewModels.MainWindowVM mainVM;

        public MenuLiveRateCommand(ViewModels.MainWindowVM main)
        {
            mainVM = main;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            mainVM.UserControl = new UesrControls.LiveRate();
        }
    }
}