using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;

namespace UI.Commands
{
    public class MenuAnimationButtonMWCommand : ICommand
    {
        ViewModels.MainWindowVM mainVM;

        public MenuAnimationButtonMWCommand(ViewModels.MainWindowVM main)
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
            if ((string)parameter == "LiveRateButton")
                mainVM.UserControl = new UesrControls.LiveRate();
            else if ((string)parameter == "ConversionButton")
                mainVM.UserControl = new UesrControls.ConvertionCurrencies();
            else if ((string)parameter == "HistoricalButton")
                mainVM.UserControl = new UesrControls.HistoricalData();
        }
    }
}
