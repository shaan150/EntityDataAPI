// Models/Entity/EntityFilter.cs
using System.Linq.Expressions;
using EntityDataAPI.Enums;
using EntityDataAPI.Models.Entity;

namespace EntityDataAPI.Filters;

public class EntityFilter : BaseFilter<Entity, EntitySortableProperties>
{
    public string? Gender { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public List<string>? Countries { get; set; }

    public override IQueryable<Entity> ApplyFilters(IQueryable<Entity> query)
    {
        query = ApplyPagination(query);
        query = ApplyGenderFilter(query);
        query = ApplyDateRangeFilter(query);
        query = ApplyCountryFilter(query);
        query = ApplySort(query);

        return query;
    }

    private IQueryable<Entity> ApplyGenderFilter(IQueryable<Entity> query)
    {
        if (string.IsNullOrEmpty(Gender))
            return query;

        return query.Where(e => e.Gender == Gender);
    }

    private IQueryable<Entity> ApplyDateRangeFilter(IQueryable<Entity> query)
    {
        if (!StartDate.HasValue && !EndDate.HasValue)
            return query;


        if (StartDate.HasValue)
        {
            query = query.Where(e => e.Dates != null || e.Dates.Any(d => d.DateTime >= StartDate));
        }

        if (EndDate.HasValue)
        {
            query = query.Where(e => e.Dates != null || e.Dates.Any(d => d.DateTime <= EndDate));
        }

        return query;
    }

    private IQueryable<Entity> ApplyCountryFilter(IQueryable<Entity> query)
    {
        if (Countries == null || Countries.Count == 0)
            return query;

        var validCountries = Countries.Where(c => !string.IsNullOrEmpty(c)).ToList();
        if (validCountries.Count == 0)
            return query;

        return query.Where(e => e.Addresses != null || e.Addresses.Any(a => a != null && validCountries.Contains(a.Country)));
    }
}

