using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace UI.Commands
{
    public class MenuConvertionCommand : ICommand
    {
        ViewModels.MainWindowVM mainVM;

        public MenuConvertionCommand(ViewModels.MainWindowVM main)
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
            mainVM.UserControl = new UesrControls.ConvertionCurrencies();
        }
    }
}
