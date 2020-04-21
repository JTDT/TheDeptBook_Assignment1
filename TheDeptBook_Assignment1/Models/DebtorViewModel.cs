using System;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Windows;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using TheDeptBook_Assignment1.DTO;

namespace TheDeptBook_Assignment1.Models
{
    public class DebtorViewModel : BindableBase
    {
        private ObservableCollection<Debtor> _debtors;
        private string _name;
        private double _debtValue;

        public DebtorViewModel(string title, ObservableCollection<Debtor> debtors)
        {
            Title = title;
            _debtors = debtors;
        }

        private string _title;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                SetProperty(ref _name, value);
            }
        }

        public double DebtValue
        {
            get { return _debtValue; }
            set { SetProperty(ref _debtValue, value); }
        }

        private ICommand _saveCommand;
        public ICommand SaveCommand
        {
            get
            {
                return _saveCommand ?? (_saveCommand = new DelegateCommand((() =>
                {
                    try
                    {
                        _debtors.Add(new Debtor(_name, _debtValue));
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Invalid name and debt value!");
                    }
                })));
            }
        }
    }
}
