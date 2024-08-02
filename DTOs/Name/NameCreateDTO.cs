namespace EntityDataAPI.DTOs;

public class NameCreateDTO
{
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string? Surname { get; set; }
    public required string EntityID { get; set; }
}
