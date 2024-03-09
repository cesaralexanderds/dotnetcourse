using BankAPI.Data;
using BankAPI.Data.BankModels;
using BankAPI.Data.DTOs;
using Microsoft.EntityFrameworkCore;
using TestBankAPI.Data.DTOs;

namespace BankAPI.Services;

public class LoginService{

    public readonly BankContext _context;

    public LoginService(BankContext context){

        _context = context;

    }

    public async Task<Administrator?> GetAdmin(AdminDTO admin){

        return await _context.Administrators.SingleOrDefaultAsync(x => x.Email == admin.email && x.Pwd == admin.Pwd);

    }

     public async Task<Client?> GetClient(ClientDTO client){

        return await _context.Clients.SingleOrDefaultAsync(x => x.Email == client.email && x.Pwd == client.Pwd);

    }

}