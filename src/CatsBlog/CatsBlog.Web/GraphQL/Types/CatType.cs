using CatsBlog.Web.Data.Models;
using CatsBlog.Web.Services;
using GraphQL.DataLoader;
using GraphQL.Types;

namespace CatsBlog.Web.GraphQL.Types
{
    public class CatType : ObjectGraphType<Cat>
    {
        public CatType()
        {
            Name = "Cat";
            Field(t => t.Id).Description("Cat id");
            Field(t => t.Name).Description("The name of the cat");
            Field(t => t.Age).Description("The age of the cat");
            Field(t => t.BirthDate).Description("The birthdate of the cat");
            Field<ColorEnum>("Color", "The color type of cat");
            Field(t => t.Owner, type: typeof(OwnerType)).Description("Owner of the cat");
        }
    }
}
