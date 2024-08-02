using EntityDataAPI.Models.Entity;

namespace EntityDataAPI.DTOs;

public class AddressDetailDTO
{
    public required string AddressId { get; set; }
    public string? AddressLine { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
    public required string EntityId { get; set; }
}
