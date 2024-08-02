namespace EntityDataAPI.DTOs;

public class NameDetailDTO
{
    public required string NameID { get; set; }
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string? Surname { get; set; }
    public required string EntityId { get; set; }
}