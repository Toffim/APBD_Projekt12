using APBD_Projekt12.Data;
using APBD_Projekt12.DTO;
using APBD_Projekt12.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD_Projekt12.Services;

public class DbService : IDbService
{
    private readonly S30519Context _context;

    public DbService(S30519Context context)
    {
        _context = context;
    }

    public async Task<TripsPaginatedDTO> GetTripsAsync(int page, int pageSize)
    {
        if (page <= 0) page = 1;
        if (pageSize <= 0) pageSize = 10;

        var totalTrips = await _context.Trips.CountAsync();
        var totalPages = (int)Math.Ceiling(totalTrips / (double)pageSize);

        // To remember how does it work, a little comment I need
        // .Skip is a param for skipping the trips from previous pages, so e.g. 1,2,3,4,5 <- we skip previous
        // and only get from current page
        // .Take(pageSize) takes only pageSize trips for the current page we got
        var trips = await _context.Trips
            .Include(t => t.IdCountries)
            .Include(t => t.ClientTrips)
            .ThenInclude(ct => ct.IdClientNavigation)
            .OrderByDescending(t => t.DateFrom)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(t => new TripDTO()
            {
                Name = t.Name,
                Description = t.Description,
                DateFrom = t.DateFrom,
                DateTo = t.DateTo,
                MaxPeople = t.MaxPeople,
                Countries = t.IdCountries.Select(c => new CountryDto { Name = c.Name }).ToList(),
                Clients = t.ClientTrips.Select(ct => new ClientDto
                {
                    FirstName = ct.IdClientNavigation.FirstName,
                    LastName = ct.IdClientNavigation.LastName
                }).ToList()
            })
            .ToListAsync();

        return new TripsPaginatedDTO()
        {
            PageNum = page,
            PageSize = pageSize,
            AllPages = totalPages,
            Trips = trips
        };
    }
}