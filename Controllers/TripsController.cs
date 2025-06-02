using APBD_Projekt12.Data;
using APBD_Projekt12.DTO;
using APBD_Projekt12.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APBD_Projekt12.Controllres;

[Route("api/[controller]")]
[ApiController]
public class TripsController : ControllerBase
{
    private readonly IDbService _dbService;

    public TripsController(IDbService dbService)
    {
        _dbService = dbService;
    }

    // /api/trips?page=1&pageSize=50
    [HttpGet]
    public async Task<ActionResult<TripsPaginatedDTO>> GetTrips([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var trips = await _dbService.GetTripsAsync(page, pageSize);
        return Ok(trips);
    }

    // /api/trips/id/clients
    [HttpPost("{idTrip}/clients")]
    public async Task<IActionResult> AssignClientToTrip(int idTrip, [FromBody] AssignClientToTripDTO dto)
    {
        return await _dbService.AssignClientToTripAsync(idTrip, dto);
    }
}