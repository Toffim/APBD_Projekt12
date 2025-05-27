using APBD_Projekt12.DTO;

namespace APBD_Projekt12.Services;

public interface IDbService
{
    Task<TripsPaginatedDTO> GetTripsAsync(int page, int pageSize);
}