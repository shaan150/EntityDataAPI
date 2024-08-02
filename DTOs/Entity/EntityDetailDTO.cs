namespace EntityDataAPI.DTOs;

public class EntityDetailDTO
{
    public required string EntityID { get; set; }
    public bool Deceased { get; set; }
    public string? Gender { get; set; }
    public List<AddressDetailDTO>? Addresses { get; set; }
    public List<DateDetailDTO>? Dates { get; set; }
    public List<NameDetailDTO>? Names { get; set; }
}
