using EntityDataAPI.Enums;
using EntityDataAPI.Models.Entity;

namespace EntityDataAPI.Filters;

public class AddressFilter : BaseFilter<Address, AddressSortableProperties>
{
    public string? AddressLine { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
    public string? EntityID { get; set; }

    public override IQueryable<Address> ApplyFilters(IQueryable<Address> query)
    {
        query = ApplyPagination(query);
        query = FilterAddressLine(query);
        query = FilterCity(query);
        query = FilterCountry(query);
        query = FilterEntityID(query);
        query = ApplySort(query);

        return query;
    }

    private IQueryable<Address> FilterAddressLine(IQueryable<Address> query)
    {
        if (string.IsNullOrEmpty(AddressLine))
        {
            return query;
        }

        return query.Where(x => x.AddressLine.Contains(AddressLine));
    }

    private IQueryable<Address> FilterCity(IQueryable<Address> query)
    {
        if (string.IsNullOrEmpty(City))
        {
            return query;
        }

        return query.Where(x => x.City.Contains(City));
    }

    private IQueryable<Address> FilterCountry(IQueryable<Address> query)
    {
        if (string.IsNullOrEmpty(Country))
        {
            return query;
        }

        return query.Where(x => x.Country.Contains(Country));
    }

    private IQueryable<Address> FilterEntityID(IQueryable<Address> query)
    {
        if (string.IsNullOrEmpty(EntityID))
        {
            return query;
        }

        return query.Where(x => x.EntityID == EntityID);
    }
    
}
