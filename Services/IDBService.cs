using APBD_Projekt12.DTO;
using Microsoft.AspNetCore.Mvc;

namespace APBD_Projekt12.Services;

public interface IDbService
{
    Task<TripsPaginatedDTO> GetTripsAsync(int page, int pageSize);
    Task<IActionResult> DeleteClientAsync(int idClient);
    Task<IActionResult> AssignClientToTripAsync(int idTrip, AssignClientToTripDTO dto);
}