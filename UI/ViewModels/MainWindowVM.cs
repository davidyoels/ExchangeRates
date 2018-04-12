using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace UI.ViewModels
{
    public class MainWindowVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Models.MainModel mainModel;
        public Commands.MenuAnimationButtonMWCommand ConvertionCommand { get; set; }
        public Commands.MenuAnimationButtonMWCommand LiveRateCommand { get; set; }
        public Commands.MenuAnimationButtonMWCommand HistoricalCommand { get; set; }

        private UserControl _UserControl;
        public UserControl UserControl
        {
            get { return _UserControl; }
            set
            {
                _UserControl = value;
                PropertyChanged(this, new PropertyChangedEventArgs(null));
            }
        }
    
        public MainWindowVM()
        {
            mainModel = new Models.MainModel();
            ConvertionCommand = new Commands.MenuAnimationButtonMWCommand(this);
            LiveRateCommand = new Commands.MenuAnimationButtonMWCommand(this);
            HistoricalCommand = new Commands.MenuAnimationButtonMWCommand(this);   
            
        }
    }
}
