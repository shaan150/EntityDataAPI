using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EntityDataAPI.Schemas;

public class EnumSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type.IsEnum)
        {
            schema.Enum = Enum.GetValues(context.Type)
                .Cast<object>()
                .Select(value => new OpenApiString($"{Enum.GetName(context.Type, value)}"))
                .ToList<IOpenApiAny>();
        }
    }
}
