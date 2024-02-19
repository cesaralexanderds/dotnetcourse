using System;

namespace BankConsole
{
    public class Client : User, IPerson
    {
        public char TaxRegime { get; set; }

        public Client(){}

        public Client(int id, string name, string email, decimal balance, char TaxRegime) : base(id, name, email, balance)
        {
            if (TaxRegime != 'M' && TaxRegime != 'S')
                throw new ArgumentException("El régimen fiscal debe ser 'M' o 'S'.");

            this.TaxRegime = TaxRegime;
            SetBalance(balance);
        }

        public override string ShowData()
        {
            return base.ShowData() + $" Regimen fiscal: {this.TaxRegime}";
        }

        public string GetName()
        {
            return Name;
        }

        public string GetCountry()
        {
            throw new NotImplementedException();
        }
    }
}
