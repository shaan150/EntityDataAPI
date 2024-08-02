using EntityDataAPI.Enums;
using EntityDataAPI.Models.Entity;

namespace EntityDataAPI.Filters
{
    public class SearchFilter : BaseFilter<Entity, EntitySortableProperties>
    {
        public string? SearchQuery { get; set; }
        public bool GetDetails { get; set; } = false;

    }
}
