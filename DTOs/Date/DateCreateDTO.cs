namespace EntityDataAPI.DTOs;

public class DateCreateDTO
{
    public string? DateType { get; set; }
    public DateTime? DateTime { get; set; }
    public required string EntityID { get; set; }
}
