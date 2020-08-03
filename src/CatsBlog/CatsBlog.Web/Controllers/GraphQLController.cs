using CatsBlog.Web.Models;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CatsBlog.Web.Controllers
{
    [Route("graphqltest")]
    [ApiController]
    public class GraphQLController : Controller
    {
        private readonly ISchema schema;
        private readonly IDocumentExecuter executer;

        public GraphQLController(ISchema schema, IDocumentExecuter executer)
        {
            this.schema = schema;
            this.executer = executer;
        }

        [HttpPost]
        public async Task<ActionResult> Post(GraphQLQuery query)
        {
            if (query is null)
            {
                return BadRequest();
            }

            var result = await this.executer.ExecuteAsync(options =>
            {
                options.Schema = this.schema;
                options.Query = query.Query;
                options.OperationName = query.OperationName;
                options.Inputs = query.Variables.ToInputs();
            });

            if (result.Errors?.Count > 0)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
