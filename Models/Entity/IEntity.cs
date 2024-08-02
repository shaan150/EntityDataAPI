namespace EntityDataAPI.Models.Entity;

public interface IEntity
{
    string EntityID { get; set; }
    List<Address>? Addresses { get; set; }
    List<Date> Dates { get; set; }
    bool Deceased { get; set; }
    string? Gender { get; set; }
    List<Name> Names { get; set; }
}
