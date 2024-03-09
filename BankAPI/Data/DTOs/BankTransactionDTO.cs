using System.ComponentModel.DataAnnotations;
using BankAPI.Data.BankModels;

namespace BankAPI.Data.DTOs;

public class BankTransactionDTO{

    public int TransactionType {get ; set; } 

    public int AccountID {get ; set; }

    public decimal Amount {get ; set; }

    public int? ExternalAccount {get ; set; } = null!;


    public DateTime RegDate {get ; set; }
    

}