namespace EntityDataAPI.Exceptions.CRUD;

public class MissingSearchQueryException : Exception
{
    public MissingSearchQueryException() : base("Search query is missing.")
    {
    }
}
