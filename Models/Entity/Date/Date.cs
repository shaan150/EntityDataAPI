namespace EntityDataAPI.Models.Entity;

public class Date
{
    public required string DateID { get; set; }
    public string? DateType { get; set; }
    public DateTime? DateTime { get; set; }
    public required string EntityId { get; set; }
    public Entity? Entity { get; set; }

}
