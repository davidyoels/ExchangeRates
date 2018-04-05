using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DP;
namespace UI.ViewModels
{
    public class LiveRateViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Models.LiveRatingModel LRModel;

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

        public Commands.ClearListCommand ClearList { get; set; }

        private ObservableCollection<DBCurrency> _CurrencyObservable;
        public ObservableCollection<DBCurrency> CurrencyObservable
        {
            get { return _CurrencyObservable; }
            set
            {
                _CurrencyObservable = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrencyObservable"));
            }
        }

        public ObservableCollection<DBCurrency> REALCurrencyObservable;
        private ObservableCollection<DBCurrency> _LVList;
        public ObservableCollection<DBCurrency> LVList
        {
            get { return _LVList; }
            set
            {
                _LVList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LVList"));
            }
        }

        private DBCurrency _SelectedCurrency;
        public DBCurrency SelectedCurrency
        {
            get { return _SelectedCurrency; }
            set
            {
                _SelectedCurrency = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedCurrency"));
                if (value != null)
                {
                    LVList.Add(value);
                    CurrencyObservable.Remove(value);
                }

            }
        }

        public LiveRateViewModel()
        {
            LRModel = new Models.LiveRatingModel();
            ClearList = new Commands.ClearListCommand(this);
            CurrencyObservable = new ObservableCollection<DBCurrency>();
            LVList = new ObservableCollection<DBCurrency>();
            LoadCurrencyList();
        }

        public async Task LoadCurrencyList()
        {
            CurrencyList = await LRModel.GetCurrencies();
            REALCurrencyObservable = new ObservableCollection<DBCurrency>();
            foreach (var item in CurrencyList)
            {
                REALCurrencyObservable.Add(item);
            }
            CurrencyObservable = REALCurrencyObservable;
        }

        public void ClearListView()
        {
            CurrencyObservable = new ObservableCollection<DBCurrency>();
            CurrencyObservable = REALCurrencyObservable;
            LVList = new ObservableCollection<DBCurrency>();
        }

        public void RemoveCurrencyFromLV(DBCurrency cur)
        {
            LVList.Remove(cur);
        }

        public void AddCurrencyToCombo(DBCurrency cur)
        {

        }
    }
}
