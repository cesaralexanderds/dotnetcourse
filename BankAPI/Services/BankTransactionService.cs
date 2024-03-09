using BankAPI.Data;
using BankAPI.Data.BankModels;
using BankAPI.Data.DTOs;
using Microsoft.EntityFrameworkCore;
using TestBankAPI.Data.DTOs;
using System.Text.Json;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Runtime.InteropServices;

namespace BankAPI.Services;

public class BankTransactionService
{

    private readonly BankContext _context;
    private readonly AccountService accountService;
    
    public BankTransactionService(BankContext context, AccountService accountService)
    {
        _context = context;
        this.accountService = accountService;

    } 

    public async Task<IEnumerable<BankTransaction>> GetAll(){
        return await _context.BankTransactions.ToListAsync();
    }

    public async Task<BankTransaction> GetById(int id){
        return await _context.BankTransactions.FindAsync(id);

    }

        public async Task<Account> GetAccountID(int id){
            return await _context.Accounts.FindAsync(id);
    }

    public async Task<BankTransaction> Withdraw(BankTransactionDTO bankTransactionDTO)
    {

        var newWithdraw = new BankTransaction();

        newWithdraw.AccountId = bankTransactionDTO.AccountID;
        var AccountToUpdate = await accountService.GetAccountById(newWithdraw.AccountId);
        newWithdraw.TransactionType = bankTransactionDTO.TransactionType;
        newWithdraw.Amount = bankTransactionDTO.Amount;
        AccountToUpdate.Balance -= bankTransactionDTO.Amount;
        if(bankTransactionDTO.ExternalAccount is not null)
            newWithdraw.ExternalAccount = bankTransactionDTO.ExternalAccount;

        

        _context.BankTransactions.Add(newWithdraw);
        await _context.SaveChangesAsync();

        return newWithdraw;
    }

       public async Task<BankTransaction> Deposit(BankTransactionDTO bankTransactionDTO, int id)
    {
        
        var account = await accountService.GetAccountById(id);
        if (account == null)
        {

            return null;
        }

        var newDeposit = new BankTransaction
        {
            AccountId = account.Id,
            TransactionType = bankTransactionDTO.TransactionType,
            Amount = bankTransactionDTO.Amount
        };


        await accountService.UpdateBalance(bankTransactionDTO, id);


        _context.BankTransactions.Add(newDeposit);
        
        await _context.SaveChangesAsync();

        return newDeposit;
    }




     public async Task DeleteAccount(int id)
    {   
        var AccountToDelete = await accountService.GetAccountById(id);
        
        if(AccountToDelete.Balance < 0){
            _context.Accounts.Remove(AccountToDelete);
            await _context.SaveChangesAsync();
        }

    }
    
    

    


}