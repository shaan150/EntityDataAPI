using System.Linq.Dynamic.Core;
using System.Reflection;

namespace EntityDataAPI.Filters;

public class BaseFilter<T, TSortEnum> where TSortEnum : Enum
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public TSortEnum? OrderBy { get; set; } = default;
    public bool SortDescending { get; set; } = false;

    public virtual IQueryable<T> ApplyFilters(IQueryable<T> query)
    {
        query = ApplyPagination(query);
        query = ApplySort(query);
        return query;
    }

    protected IQueryable<T> ApplyPagination(IQueryable<T> query)
    {
        if (PageNumber < 1 || PageSize < 1)
            return query;

        return query.Skip((PageNumber - 1) * PageSize)
                    .Take(PageSize);
    }

    protected IQueryable<T> ApplySort(IQueryable<T> query)
    {
        string orderBy = OrderBy.ToString() ?? throw new ArgumentNullException(nameof(OrderBy));
        var property = typeof(T).GetProperty(orderBy, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
        if (property == null)
            throw new ArgumentException($"Property '{OrderBy}' does not exist on type '{typeof(T).Name}'");

        string sortingOrder = SortDescending ? "descending" : "ascending";
        return query.OrderBy($"{orderBy} {sortingOrder}");
    }

}
