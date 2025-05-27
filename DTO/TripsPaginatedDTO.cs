namespace APBD_Projekt12.DTO;

public class TripsPaginatedDTO
{
    public int PageNum { get; set; }
    public int PageSize { get; set; }
    public int AllPages { get; set; }

    public List<TripDTO> Trips { get; set; } = new List<TripDTO>();
}