using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //a();
            this.DataContext = new ViewModels.MainWindowVM();
        }

        public async Task a()
        {
            Bl_imp ibl = new Bl_imp();

            //var rslt = await ibl.GetCurrencies().ConfigureAwait(false);
         //   var rr = await ibl.GetHistorialCurrencies("ILS").ConfigureAwait(false);
           // int x = 5;
           // x = x + 2;

        }

    }
}
