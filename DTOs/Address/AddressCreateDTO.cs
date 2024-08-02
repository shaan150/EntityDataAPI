namespace EntityDataAPI.DTOs;

public class AddressCreateDTO
{
    public string? AddressLine { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
    public required string EntityID { get; set; }
}
