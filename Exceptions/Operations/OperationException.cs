namespace EntityDataAPI.Exceptions.Operations;

public class OperationException : Exception
{
    public OperationException()
    {
    }

    public OperationException(string message)
        : base(message)
    {
    }

    public OperationException(string message, Exception inner)
        : base(message, inner)
    {
    }
}
