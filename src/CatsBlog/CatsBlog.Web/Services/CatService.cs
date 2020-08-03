using CatsBlog.Web.Data;
using CatsBlog.Web.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatsBlog.Web.Services
{
    public class CatService : ICatService
    {
        private readonly CatsDbContext dbContext;

        public CatService(CatsDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IReadOnlyList<Cat>> AllCatsAsync()
            => await this.dbContext.Cats
                .Include(c => c.Owner)
                .ToListAsync();

        public async Task<IReadOnlyList<Owner>> AllOwnersAsync()
            => await this.dbContext.Owners
                .Include(o => o.Cats)
                .ToListAsync();

        public async Task<Owner> GetOwnerAsync(int id)
            => await this.dbContext.Owners
                .FindAsync(id);

        public async Task<ILookup<int, Cat>> GetOwnerCatsAsync(IEnumerable<int> ownersIds)
        {
            var cats = await this.dbContext.Cats
                .Where(c => ownersIds.Contains(c.OwnerId))
                .ToListAsync();

            return cats.ToLookup(c => c.OwnerId);
        }
    }
}
