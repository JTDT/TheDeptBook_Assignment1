using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using TheDeptBook_Assignment1.DTO;

namespace TheDeptBook_Assignment1.Models
{
    public class DebtorDetailsWindowViewModel : BindableBase
    {
        private ObservableCollection<DebtorsDebt> _debtorsDebts;
        private Debtor _debtor;
        private double _debtValue;
        private DebtorsDebt debt;
        public DebtorDetailsWindowViewModel(string title, Debtor debtor)
        {
            Title = title;
            _debtor = debtor;
            _debtorsDebts = new ObservableCollection<DebtorsDebt>(_debtor._debtorsDebts);
        }

        private string _title;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }


        public Debtor CurrentDebtor
        {
            get { return _debtor; }
            set { SetProperty(ref _debtor, value); }
        }


        public ObservableCollection<DebtorsDebt> DebtorsDebts
        {
            get { return _debtorsDebts; }
            set { SetProperty(ref _debtorsDebts, value); }
        }

        public double NewDebtValue
        {
            get { return _debtValue; }
            set
            {
                SetProperty(ref _debtValue, value);
            }
        }

        ICommand _addDebtCommand;

        public ICommand AddDebtCommand
        {
            get
            {
                return _addDebtCommand ?? (_addDebtCommand = new DelegateCommand(() =>
                {
                    try
                    {
                        debt = new DebtorsDebt(DateTime.Today, _debtValue);
                        _debtor.addTotalDebt(_debtValue);
                        DebtorsDebts.Add(debt);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Invalid value. Try again!");
                    }

                }));
            }
        }
    }
}
