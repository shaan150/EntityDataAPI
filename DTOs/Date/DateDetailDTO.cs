namespace EntityDataAPI.DTOs;
public class DateDetailDTO
{
    public required string DateID { get; set; }
    public string? DateType { get; set; }
    public DateTime? DateTime { get; set; }
    public required string EntityId { get; set; }
}
