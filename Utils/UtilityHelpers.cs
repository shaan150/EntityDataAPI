using EntityDataAPI.Exceptions.Operations;
using EntityDataAPI.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace EntityDataAPI.Utils;

public static class UtilityHelpers
{
    public static string GenerateNewGuid(string prefix)
    {
        // get datetime now 
        var now = DateTime.Now;

        // generate new guid
        return string.Concat(prefix, now.ToString("yyyyMMddHHmmssfff"), Guid.NewGuid().ToString().AsSpan(0, 25));
    }

    public static bool FindMatchingToQuery(string query, Entity e)
    {
        return e.Names.Any(n =>
                               (n.FirstName ?? "").Contains(query, StringComparison.OrdinalIgnoreCase) ||
                               (n.MiddleName ?? "").Contains(query, StringComparison.OrdinalIgnoreCase) ||
                               (n.Surname ?? "").Contains(query, StringComparison.OrdinalIgnoreCase)) ||
                               e.Addresses.Any(a =>
                               (a.AddressLine ?? "").Contains(query, StringComparison.OrdinalIgnoreCase) ||
                               (a.Country ?? "").Contains(query, StringComparison.OrdinalIgnoreCase));
    }

    public static async Task HandleOperationAsync(Func<Task> operation, string errorMessage)
    {
        var exceptionMap = new OperationExceptionMap();

        try
        {
            await operation();
        }
        catch (Exception ex)
        {
            var specificError = exceptionMap.GetExceptionMessage(ex);
            throw new OperationException($"{errorMessage}: {specificError}", ex);
        }
    }


}
