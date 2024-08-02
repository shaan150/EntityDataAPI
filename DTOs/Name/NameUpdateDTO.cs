namespace EntityDataAPI.DTOs;

public class NameUpdateDTO
{
    public required string NameID { get; set; }
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string? Surname { get; set; }
}
