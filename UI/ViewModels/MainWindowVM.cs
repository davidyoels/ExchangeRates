using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace UI.ViewModels
{
    public class MainWindowVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Models.MainModel mainModel;
        public Commands.MenuConvertionCommand ConvertionCommand { get; set; }
        public Commands.MenuLiveRateCommand LiveRateCommand { get; set; }
        public Commands.MenuHistoricalCommand HistoricalCommand { get; set; }

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
            ConvertionCommand = new Commands.MenuConvertionCommand(this);
            LiveRateCommand = new Commands.MenuLiveRateCommand(this);
            HistoricalCommand = new Commands.MenuHistoricalCommand(this);
        }
    }
}
