using BankAPI.Data;
using BankAPI.Data.BankModels;
using BankAPI.Data.DTOs;
using Microsoft.EntityFrameworkCore;
using TestBankAPI.Data.DTOs;

namespace BankAPI.Services;


public class AccountService
{

    private readonly BankContext _context;
    public AccountService(BankContext context)
    {
        _context = context;


    }

    public async Task<IEnumerable<AccountDtoOut>> GetAll()
    {
        return await _context.Accounts.Select(a => new AccountDtoOut
        {
            Id = a.Id,
            AccountName = a.AccountTypeNavigation.Name,
            ClientName = a.Client != null ? a.Client.Name : "",
            Balance = a.Balance,
            RegDate = a.RegDate
        }).ToListAsync();
    }

    public async Task<AccountDtoOut> GetDtoById(int id)
    {
        return await _context.Accounts.
        Where(a => a.Id == id).
        Select(a => new AccountDtoOut
        {
            Id = a.Id,
            AccountName = a.AccountTypeNavigation.Name,
            ClientName = a.Client != null ? a.Client.Name : "",
            Balance = a.Balance,
            RegDate = a.RegDate
        }).SingleOrDefaultAsync();
    }

        public async Task<Account> GetAccountById(int id)
    {
        return await _context.Accounts.
        Where(a => a.Id == id).
        Select(a => new Account
        {
            Id = a.Id,
            ClientId = a.Client != null ? a.Client.Id : 0,
            Balance = a.Balance,
            RegDate = a.RegDate
        }).SingleOrDefaultAsync();
    }

    public async Task UpdateBalance(BankTransactionDTO bankTransactionDTO, int id)
    {   
        var AccountToUpdate = await GetAccountById(id);
        
        if(AccountToUpdate == null)
            return;
        


        AccountToUpdate.Balance += bankTransactionDTO.Amount;
        _context.Accounts.Add(AccountToUpdate);

        await _context.SaveChangesAsync();

        return;


    }


    public async Task<Account> GetByID(int id)
    {
        return await _context.Accounts.FindAsync(id);
    }
    public async Task<Account> Create(AccountDtoIn newAccountDTO)
    {

        var newAccount = new Account();

        newAccount.AccountType = newAccountDTO.AccountType;
        newAccount.ClientId = newAccountDTO.ClientId;
        newAccount.Balance = newAccountDTO.Balance;

        _context.Accounts.Add(newAccount);
        await _context.SaveChangesAsync();

        return newAccount;
    }
    public async Task Update(AccountDtoIn Account)
    {

        var ExistingAccount = await GetByID(Account.Id);
        if (ExistingAccount is not null)
        {

            ExistingAccount.AccountType = Account.AccountType;
            ExistingAccount.ClientId = Account.ClientId;
            ExistingAccount.Balance = Account.Balance;

            await _context.SaveChangesAsync();
        }
    }
    public async Task Delete(int id)
    {

        var AccountToDelete = await GetByID(id);
        if (AccountToDelete is not null)
        {

            _context.Accounts.Remove(AccountToDelete);
            await _context.SaveChangesAsync();

        }
    }
}
