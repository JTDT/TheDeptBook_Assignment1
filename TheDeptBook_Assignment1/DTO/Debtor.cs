using System;
using System.Collections.Generic;
using Prism.Mvvm;

namespace TheDeptBook_Assignment1.DTO
{
    public class Debtor : BindableBase
    {
        private string _name;
        private double _debt;
        public List<DebtorsDebt> _debtorsDebts;
        public Debtor(string name, double debt)
        {
            _name = name;
            _debtorsDebts = new List<DebtorsDebt>();
            if (debt != 0)
            {
                addTotalDebt(debt);
            }
            _debt = debt;

        }

        /// <summary>
        /// Performs only a shallow copy
        /// </summary>
        /// <returns></returns>
        public Debtor Clone()
        {
            return this.MemberwiseClone() as Debtor;
        }

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        public double Debt
        {
            get { return _debt; }
            set
            {
                SetProperty(ref _debt, value);

            }
        }

        public void addTotalDebt(double totaltDebt)
        {

            _debtorsDebts.Add(new DebtorsDebt(DateTime.Today, totaltDebt));
            _debt += totaltDebt;
        }
    }
}
