using BankAPI.Data;
using BankAPI.Data.BankModels;
using Microsoft.EntityFrameworkCore;

namespace BankAPI.Services;


public class ClientService{

    private readonly BankContext _context;
    public ClientService(BankContext context){
        _context = context;

        
    }

    public async Task<IEnumerable<Client>> GetAll(){
        return await _context.Clients.ToListAsync();
    }
    public async Task<Client> GetByID(int id){
        return  await _context.Clients.FindAsync(id);
    }
    public async Task<Client> Create(Client newClient){

        _context.Clients.Add(newClient);
        await _context.SaveChangesAsync();

        return newClient;
    }
    public async Task Update(int id, Client client){
       
        var ExistingClient = await GetByID(id);
        if(ExistingClient is not null){
        
            ExistingClient.Name = client.Name;
            ExistingClient.PhoneNumber = client.PhoneNumber;
            ExistingClient.Email = client.Email;
        
            await _context.SaveChangesAsync();
        }
    }
    public async Task Delete(int id){

        var clientToDelete = await GetByID(id);
        if(clientToDelete is not null){

        _context.Clients.Remove(clientToDelete);
        await _context.SaveChangesAsync();

        }
    }
}
