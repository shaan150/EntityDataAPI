using Microsoft.EntityFrameworkCore;

namespace EntityDataAPI.Exceptions.Operations;

public class OperationExceptionMap
{
    private readonly Dictionary<Type, string> _exceptionMessages;

    public OperationExceptionMap()
    {
        _exceptionMessages = new Dictionary<Type, string>
    {
        { typeof(DbUpdateConcurrencyException), "Concurrency Error:" },
        { typeof(DbUpdateException), "Database Update Error:" },
        { typeof(InvalidOperationException), "Invalid Operation:" },
        { typeof(Exception), "An Unexpected Error Occurred:"}
    };
    }

    public void AddExceptionMessage<TException>(string message) where TException : Exception
    {
        _exceptionMessages[typeof(TException)] = message;
    }

    public string GetExceptionMessage(Exception ex)
    {
        return _exceptionMessages.TryGetValue(ex.GetType(), out var message)
            ? message + $" - {ex.Message}"
            : "System Exception Occured: " + $" - {ex.Message}";
    }
}
