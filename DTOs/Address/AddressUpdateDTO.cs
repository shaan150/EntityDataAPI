namespace EntityDataAPI.DTOs;

public class AddressUpdateDTO
{
    public required string AddressID { get; set; }
    public string? AddressLine { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
}
