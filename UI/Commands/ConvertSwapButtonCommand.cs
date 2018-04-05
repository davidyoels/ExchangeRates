﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace UI.Commands
{
    public class ConvertSwapButtonCommand : ICommand
    {
        ViewModels.ConvertionCrrViewModel conVM;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public ConvertSwapButtonCommand(ViewModels.ConvertionCrrViewModel vm)
        {
            this.conVM = vm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            conVM.GetConvertSwap();
        }
    }
}
