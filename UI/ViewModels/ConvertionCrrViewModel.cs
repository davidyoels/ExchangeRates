using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DP;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BL;

namespace UI.ViewModels
{
    public class ConvertionCrrViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        //public Models.LiveRatingModel LRModel;
        public Models.ConvertionModel ConModel;
        public Commands.ConvertApplyButtonCommand ConversionActCommand { get; set; }
        public Commands.ConvertSwapButtonCommand SwapCommand { get; set; }

        //defintions
        private List<DBCurrency> _CurrencyList;
        public List<DBCurrency> CurrencyList
        {
            get { return _CurrencyList; }
            set
            {
                _CurrencyList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrencyList"));
            }
        }

        public DBCurrency From
        {
            get { return _from; }
            set
            {
                _from = value;
                FlagFrom = @"C:\Users\david salmon\ExchangeRates\UI\Images\" + _from.Initials + ".png";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("From"));
            }
        }

        public DBCurrency To
        {
            get { return _to; }
            set
            {
                _to = value;
                FlagTo = @"C:\Users\david salmon\ExchangeRates\UI\Images\" + _to.Initials + ".png";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("To"));
            }
        }

        public string Amount
        {
            get { return _amount; }
            set
            {
                _amount = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Amount"));
            }
        }

        public string ConversionResult
        {
            get { return _conversionResult; }
            set
            {
                _conversionResult = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ConversionResult"));
            }
        }
        private String _flagFrom;
        public String FlagFrom
        {
            get { return _flagFrom; }
            set
            {
                _flagFrom = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FlagFrom"));
            }
        }

        private String _flagTo;
        public String FlagTo
        {
            get { return _flagTo; }
            set
            {
                _flagTo = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FlagTo"));
            }
        }

        private DBCurrency _from;
        private DBCurrency _to;
        private string _amount;
        private string _conversionResult;


        //functions to be execute by calling one of the commands in the Commmands folder.
        public ConvertionCrrViewModel()
        {
            LoadCurrencyList();
            ConversionActCommand = new Commands.ConvertApplyButtonCommand(this);
            SwapCommand = new Commands.ConvertSwapButtonCommand(this);
            ConModel = new Models.ConvertionModel();
            
        }

        public async Task LoadCurrencyList()
        {
            CurrencyList = await new Models.LiveRatingModel().GetCurrencies();
        }

        public void GetConvertCalculation()
        {
            ConversionResult = Convert.ToString(ConModel.GetConvertCalculation(From, To, Amount));
        }

        public void GetConvertSwap()
        {
            var temp = To;
            To = From;
            From = temp;
            GetConvertCalculation();
        }
       
    }
}
