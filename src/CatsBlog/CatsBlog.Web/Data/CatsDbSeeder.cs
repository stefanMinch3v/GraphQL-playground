using CatsBlog.Web.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CatsBlog.Web.Data
{
    public static class CatsDbSeeder
    {
        public static void Seed(this CatsDbContext db)
        {
            db.Database.Migrate();

            if (db.Cats.Any())
            {
                return;
            }

            for (int i = 1; i <= 100; i++)
            {
                var owner = new Owner
                {
                    Name = $"Owner {i}"
                };

                for (int j = 1; j <= 10; j++)
                {
                    owner.Cats.Add(new Cat
                    {
                        Name = $"Cat {i} {j}",
                        Color = j % 2 == 0 ? Color.Black : Color.White,
                        BirthDate = DateTime.Now.AddDays(-j),
                        Age = j
                    });
                }

                db.Owners.Add(owner);

                if (i % 2 == 0)
                {
                    db.SaveChanges();
                    Console.Write(".");
                }
            }
        }
    }
}
