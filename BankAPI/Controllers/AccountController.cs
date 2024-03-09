using Microsoft.AspNetCore.Mvc;
using BankAPI.Services;
using BankAPI.Data.BankModels;
using Microsoft.Extensions.FileProviders;
using TestBankAPI.Data.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace BankAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase{

    private readonly AccountService accountService;
    private readonly AccountTypeService accountTypeService;
    private readonly ClientService clientService;



    public AccountController(ClientService clientService, AccountTypeService accountTypeService, AccountService accountService){
        this.accountService = accountService;
        this.accountTypeService = accountTypeService;
        this.clientService = clientService;
    }

    [HttpGet("get")]
    public async Task<IEnumerable<AccountDtoOut>> Get(){
        return await accountService.GetAll();
    }

     [HttpGet("{id}")]
    public async Task<ActionResult<AccountDtoOut>> GetByID(int id){
        var Account =  await accountService.GetDtoById(id);
        
        if(Account is null)
            return NotFound(new {message = $"El cliente con el ID {id} no existe."});
        
        return Account;
    }

    [Authorize(Policy = "SuperAdmin")]
    [HttpPost("create")]
    public async Task<IActionResult> Create(AccountDtoIn account){

        string  validationResult =  await ValidateAccount(account);

        if(!validationResult.Equals("Valid"))
            return BadRequest(new {message = validationResult});

        var newAccount =  await accountService.Create(account);

        return CreatedAtAction(nameof(GetByID), new {id = newAccount.Id}, newAccount);
    }

    [Authorize(Policy = "SuperAdmin")]
    [HttpPut("update/{id}")]
    public async Task<IActionResult> Update(int id, AccountDtoIn Account){
       
        if(id != Account.Id)
            return BadRequest(new {message = $"El ID {id} de la URL no coincide con el id {Account.Id} del cuerpo de la solicitud"});
        
        var accountToUpdate = await accountService.GetByID(id);

        if(accountToUpdate is not null){
            string validationResult =  await ValidateAccount(Account);

            if(!validationResult.Equals("Valid"))
                return BadRequest();

            await accountService.Update(Account);
            return NoContent();
        }else{
            return NotFound();
        }
    }

    [Authorize(Policy = "SuperAdmin")]
    [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id){
       
        var clientToDelete = await accountService.GetByID(id);

        if(clientToDelete is not null){
            await accountService.Delete(id);
            return Ok();
        }else{
            return NotFound();
        }
    }

    public async Task<string> ValidateAccount(AccountDtoIn account){
        string result = "Valid";

        var accountType = await accountTypeService.GetByID(account.AccountType);

        if(accountType is null){
            result = $"El tipo de cuenta {account.AccountType} no existe";
        }

        var clientID = account.ClientId.GetValueOrDefault();
        var Account = await clientService.GetByID(clientID);

        if(Account is null)
            result = $"El cliente {clientID} no existe";

        return result;
    }

}