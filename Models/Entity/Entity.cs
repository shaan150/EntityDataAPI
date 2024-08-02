namespace EntityDataAPI.Models.Entity;


public class Entity : IEntity
{
    public required string EntityID { get; set; }
    public List<Address>? Addresses { get; set; }
    public required List<Date> Dates { get; set; }
    public bool Deceased { get; set; }
    public string? Gender { get; set; }
    public required List<Name> Names { get; set; }
}
