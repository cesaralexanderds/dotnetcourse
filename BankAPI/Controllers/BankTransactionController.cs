using Microsoft.AspNetCore.Mvc;
using BankAPI.Data;
using BankAPI.Data.BankModels;
using BankAPI.Services;
using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using BankAPI.Data.DTOs;
using TestBankAPI.Data.DTOs;

namespace BankAPI.Controllers;

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BankTransactionsController : ControllerBase
    {
        private readonly BankTransactionService service;
            private readonly AccountService accountService;


        public BankTransactionsController(BankTransactionService transaction, AccountService accountService)
        {
            service = transaction;
            this.accountService = accountService;
        }

        
    [HttpGet]
    public async Task<IEnumerable<BankTransaction>> GetAll(){
        return await service.GetAll();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BankTransaction>> GetById(int id){
        return await service.GetById(id);
    }



    [HttpPost("new")]
    public async Task<IActionResult> Create(BankTransactionDTO bankTransaction){

        

        var newTransaction =  await service.Withdraw(bankTransaction);

        return CreatedAtAction(nameof(GetById), new {id = newTransaction.Id}, newTransaction);
    }

    [HttpPut("deposit/{id}")]
    public async Task<IActionResult> Deposit(BankTransactionDTO bankTransaction, int id){
        
        var confirmation = await service.Deposit(bankTransaction, id);

        if(confirmation is null)
            return BadRequest();

        return Ok();
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(int id){
        var clientToDelete = await accountService.GetByID(id);

        if(clientToDelete is not null){
            await service.DeleteAccount(id);
            return Ok();
        }else{
            return NotFound();
        }
    }
    
    public async Task<int?> getAccount(BankTransaction account){
        

        var accountid = await service.GetAccountID(account.AccountId);


        return accountid.ClientId;
    }
}
