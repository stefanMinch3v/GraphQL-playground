using CatsBlog.Web.Data.Models;
using CatsBlog.Web.Services;
using GraphQL.DataLoader;
using GraphQL.Types;

namespace CatsBlog.Web.GraphQL.Types
{
    public class OwnerType : ObjectGraphType<Owner>
    {
        public OwnerType(ICatService catService, IDataLoaderContextAccessor dataLoader)
        {
            Name = "Owner";
            Field(t => t.Id).Description("Owner id");
            Field(t => t.Name).Description("The name of the owner");
            Field(t => t.Cats, type: typeof(ListGraphType<CatType>)).Description("Cats of the owner");

            Field<ListGraphType<CatType>>(
                "cats",
                resolve: context =>
                {
                    var loader =
                        dataLoader.Context.GetOrAddCollectionBatchLoader<int, Cat>(
                            "catService.GetOwnerCatsAsync", 
                            catService.GetOwnerCatsAsync);

                    return loader.LoadAsync(context.Source.Id);
                }
            );
        }
    }
}
