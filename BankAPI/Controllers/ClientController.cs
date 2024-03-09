using Microsoft.AspNetCore.Mvc;
using BankAPI.Services;
using BankAPI.Data.BankModels;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Authorization;

namespace BankAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ClientController : ControllerBase{

    private readonly ClientService _service;
    public ClientController(ClientService client){
        _service = client;
    }

    [HttpGet]
    public async Task<IEnumerable<Client>> Get(){
        return await _service.GetAll();
    }

     [HttpGet("{id}")]
    public async Task<ActionResult<Client>> GetByID(int id){
        var client =  await _service.GetByID(id);
        
        if(client is null)
            return NotFound(new {message = $"El cliente con el ID {id} no existe."});
        
        return client;
    }

    [Authorize(Policy = "SuperAdmin")]
    [HttpPost]
    public async Task<IActionResult> Create(Client client){
        var newClient =  await _service.Create(client);

        return CreatedAtAction(nameof(GetByID), new {id = newClient.Id}, newClient);
    }

    [Authorize(Policy = "SuperAdmin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Client client){
       
        if(id != client.Id)
            return BadRequest(new {message = $"El ID {id} de la URL no coincide con el id {client.Id} del cuerpo de la solicitud"});
        
        var clientToUpdate = await _service.GetByID(id);
        if(clientToUpdate is not null){
            await _service.Update(id, clientToUpdate);
            return NoContent();
        }else{
            return NotFound();
        }
    }


    [Authorize(Policy = "SuperAdmin")]
    [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id){
       
        var clientToDelete = await _service.GetByID(id);

        if(clientToDelete is not null){
            await _service.Delete(id);
            return Ok();
        }else{
            return NotFound();
        }
    }

}