using System;
using Prism.Mvvm;

namespace TheDeptBook_Assignment1.DTO
{
    public class DebtorsDebt :BindableBase
    {
        private DateTime _dateTime = System.DateTime.Today;
        private double _debt;

        public DebtorsDebt(DateTime dateTime, double debt)
        {
            _dateTime = dateTime;
            _debt = debt;
        }

        public DateTime DateTime
        {
            get { return _dateTime; }
            set { SetProperty(ref _dateTime, value); }
        }

        public double Debt
        {
            get { return _debt; }
            set { SetProperty(ref _debt, value); }
        }
    }
}
