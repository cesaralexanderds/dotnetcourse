using System.ComponentModel.DataAnnotations;

namespace BankAPI.Data.DTOs;

public class ClientDTO{

    public string email {get ; set; } = null!; 

    public string Pwd {get ; set; } = null!; 


}