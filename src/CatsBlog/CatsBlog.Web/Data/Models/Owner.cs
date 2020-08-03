using System.Collections.Generic;

namespace CatsBlog.Web.Data.Models
{
    public class Owner
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Cat> Cats { get; } = new HashSet<Cat>();
    }
}
