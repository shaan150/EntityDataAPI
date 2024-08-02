namespace EntityDataAPI.Models.Entity;

public class Address
{
    public required string AddressID { get; set; }
    public string? AddressLine { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
    public required string EntityID { get; set; }
    public Entity? Entity { get; set; }

}
