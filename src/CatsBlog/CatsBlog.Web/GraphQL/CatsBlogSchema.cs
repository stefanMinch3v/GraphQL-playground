using GraphQL;
using GraphQL.Types;

namespace CatsBlog.Web.GraphQL
{
    public class CatsBlogSchema : Schema
    {
        public CatsBlogSchema(IDependencyResolver resolver)
            : base(resolver)
        {
            Query = resolver.Resolve<CatsBlogQuery>();
        }
    }
}
