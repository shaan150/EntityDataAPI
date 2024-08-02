namespace EntityDataAPI.Models.Entity;

public class Name
{
    public required string NameID { get; set; }
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string? Surname { get; set; }
    public required string EntityID { get; set; }
    public Entity? Entity { get; set; }
}
