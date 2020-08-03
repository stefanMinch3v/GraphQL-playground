using CatsBlog.Web.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatsBlog.Web.Services
{
    public interface ICatService
    {
        Task<IReadOnlyList<Cat>> AllCatsAsync();

        Task<IReadOnlyList<Owner>> AllOwnersAsync();

        Task<Owner> GetOwnerAsync(int id);

        Task<ILookup<int, Cat>> GetOwnerCatsAsync(IEnumerable<int> ownersIds);
    }
}
