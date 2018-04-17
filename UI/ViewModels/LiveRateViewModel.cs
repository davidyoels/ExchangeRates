using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using DP;
namespace UI.ViewModels
{
    public class LiveRateViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Models.LiveRatingModel LRModel;



        private List<DBCurrency> _currencyList;
        public List<DBCurrency> CurrencyList
        {
            get { return _currencyList; }
            set
            {
                _currencyList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrencyList"));
            }
        }

        private string _flag;
        public string Flag
        {
            get { return _flag; }
            set
            {
                _flag = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Flag"));
            }
        }

        public async Task LoadCurrenciesList()
        {
            CurrencyList = await new Models.LiveRatingModel().GetCurrencies();
        }
        public LiveRateViewModel()
        {
            LoadCurrenciesList();  
        }

    

       /* public void FilterList(object sender, TextChangedEventArgs e)
        {
            TextBlock text = Live
            List<DBCurrency> newCurrencyList = new List<DBCurrency>();
            foreach (DBCurrency newCurrency in CurrencyList)
            {
                if (newCurrency.FullName.StartsWith())
                    newCurrencyList.Add(newCurrency);

            }
            CurrencyList = newCurrencyList;
        }*/
        /*
            public ConvertionCrrViewModel()
        {
            LoadCurrencyList();
            ConversionActCommand = new Commands.ConvertApplyButtonCommand(this);
            SwapCommand = new Commands.ConvertSwapButtonCommand(this);
            ConModel = new Models.ConvertionModel();
        }
         */
    }
    public class Converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string initial = value as string;
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
