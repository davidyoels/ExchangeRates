using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UI.ViewModels;

namespace UI.Commands
{
    public class LiveRateListViewCommand : ICommand
    {

        LiveRateViewModel conVM;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public LiveRateListViewCommand(ViewModels.LiveRateViewModel vm)
        {
            this.conVM = vm;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            conVM.DisplayLiveRateOnListView();
        }
    }
}
