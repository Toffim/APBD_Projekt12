using APBD_Projekt12.Data;
using APBD_Projekt12.DTO;
using APBD_Projekt12.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APBD_Projekt12.Controllres;

[Route("api/[controller]")]
[ApiController]
public class ClientsController : ControllerBase
{
    private readonly IDbService _dbService;

    public ClientsController(IDbService dbService)
    {
        _dbService = dbService;
    }

    // /api/clients/{id}
    [HttpDelete("{idClient}")]
    public async Task<IActionResult> DeleteClient(int idClient)
    {
        return await _dbService.DeleteClientAsync(idClient);
    }
}