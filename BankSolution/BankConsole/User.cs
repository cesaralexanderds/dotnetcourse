namespace BankConsole;
using Newtonsoft.Json;

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

    public User(){

    }

    public User(int id, string name, string email, decimal balance){
        this.ID = id;
        this.Name = name;
        this.Email = email;
        this.RegisterDate = DateTime.Now;
    }

    public int GetID(){
        return ID;
    }
    public DateTime getRegisterDate(){
        return RegisterDate;
    }

    public virtual string ShowData(){
        return $"ID: {this.ID}, Nombre: {this.Name}, Correo: {this.Email}, Saldo: {this.Balance}, Fecha de registro: {this.RegisterDate.ToShortDateString()}";
    }

    public string ShowData(string initialMessage){
        return $"{initialMessage} -> Nombre: {this.Name}, Correo: {this.Email}, Saldo: {this.Balance}, Fecha de registro: {this.RegisterDate}";
    }

    public virtual void SetBalance(decimal amount){
        decimal quantity = 0;

        if(amount < 0)
            quantity = 0;
        else quantity += amount;
        this.Balance += quantity;
    }


}