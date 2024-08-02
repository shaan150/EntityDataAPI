namespace EntityDataAPI.DTOs;

public class EntityUpdateDTO
{
    public required string EntityId { get; set; }
    public bool Deceased { get; set; }
    public string? Gender { get; set; }
}
