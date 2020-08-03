using CatsBlog.Web.GraphQL.Types;
using CatsBlog.Web.Services;
using GraphQL.Types;

namespace CatsBlog.Web.GraphQL
{
    public class CatsBlogQuery : ObjectGraphType
    {
        public CatsBlogQuery(ICatService catService)
        {
            Field<ListGraphType<CatType>>(
                "cats",
                resolve: context => catService.AllCatsAsync()
            );

            Field<ListGraphType<OwnerType>>(
                "owners",
                resolve: context => catService.AllOwnersAsync()
            );

            Field<OwnerType>(
                "owner",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>>
                { Name = "id" }),
                resolve: context =>
                {
                    var id = context.GetArgument<int>("id");
                    return catService.GetOwnerAsync(id);
                }
            );
        }
    }
}
