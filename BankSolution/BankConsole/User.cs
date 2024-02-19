using System;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace BankConsole
{
    public class User
    {
        [JsonProperty]
        protected int ID { get; set;}
        [JsonProperty]
        protected string Name { get; set; }
        [JsonProperty]
        protected string Email { get; set; }
        [JsonProperty]
        protected decimal Balance { get; set; }
        [JsonProperty]
        protected DateTime RegisterDate { get; set; }

        public User(){}

        public User(int id, string name, string email, decimal balance)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser un entero positivo.");

            if (!IsValidEmail(email))
                throw new ArgumentException("El correo electrónico no tiene un formato válido.");

            this.ID = id;
            this.Name = name;
            this.Email = email;
            this.RegisterDate = DateTime.Now;
            SetBalance(balance);
        }

        private bool IsValidEmail(string email)
        {
            string emailPattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
            return Regex.IsMatch(email, emailPattern);
        }

        public int GetID()
        {
            return ID;
        }

        public DateTime GetRegisterDate()
        {
            return RegisterDate;
        }

        public virtual string ShowData()
        {
            return $"ID: {this.ID}, Nombre: {this.Name}, Correo: {this.Email}, Saldo: {this.Balance}, Fecha de registro: {this.RegisterDate.ToShortDateString()}";
        }

        public string ShowData(string initialMessage)
        {
            return $"{initialMessage} -> Nombre: {this.Name}, Correo: {this.Email}, Saldo: {this.Balance}, Fecha de registro: {this.RegisterDate}";
        }

        public virtual void SetBalance(decimal amount)
        {
            if (amount < 0)
                throw new ArgumentException("El saldo debe ser un decimal positivo.");
            
            this.Balance += amount;
        }
    }
}
