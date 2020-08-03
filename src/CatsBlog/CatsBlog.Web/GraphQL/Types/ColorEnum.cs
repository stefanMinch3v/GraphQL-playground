using CatsBlog.Web.Data.Models;
using GraphQL.Types;

namespace CatsBlog.Web.GraphQL.Types
{
    public class ColorEnum : EnumerationGraphType<Color>
    {
        public ColorEnum()
        {
            Name = "Type";
            Description = "The type of cat";
        }
    }
}
