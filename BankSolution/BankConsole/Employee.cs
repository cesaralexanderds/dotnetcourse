using System;

namespace BankConsole
{
    public class Employee : User, IPerson
    {
        public string Department { get; set; }

        public Employee(){}

        public Employee(int id, string name, string email, decimal balance, string Department) : base(id, name, email, balance)
        {
            this.Department = Department;
            SetBalance(balance);
        }

        public override string ShowData()
        {
            return base.ShowData() + $", Department: {this.Department}";
        }

        public string GetName()
        {
            return Name + '!';
        }

        public string GetCountry()
        {
            throw new NotImplementedException();
        }
    }
}
