using EntityDataAPI.Enums;

namespace EntityDataAPI.Exceptions.CRUD;

public class NotFoundException : Exception
{
    public NotFoundException()
    {
    }

    public NotFoundException(TypesEnum type, string id)
        : base(FormatMessage(type, id))
    {
    }

    public NotFoundException(TypesEnum type, string id, Exception inner)
        : base(FormatMessage(type, id), inner)
    {
    }

    private static string FormatMessage(TypesEnum type, string id) => $"{type} with ID {id} not found"; 
}
