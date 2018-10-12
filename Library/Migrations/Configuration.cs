namespace Library.Migrations
{
    using Library.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Library.Models.LibraryContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Library.Models.LibraryContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            //Genre addGenre = new Genre();
            //addGenre.Id = 3;
            //addGenre.Name = "Comedy";
            //context.Genres.Add(addGenre);
            //addGenre.Id = 2;
            //addGenre.Name = "Comedy";
            //context.Genres.Add(addGenre);
            //addGenre.Id = 1;
            //addGenre.Name = "Horror";
            //context.Genres.Add(addGenre);

            context.Branches.AddOrUpdate(new Models.Branch()
            {
                Id = 1,
                Address = "Raanana",
                lat = 32.18489140558821,
                lng = 34.87343421596455
            });
            context.Branches.AddOrUpdate(new Models.Branch()
            {
                Id = 2,
                Address = "Rehovot",
                lat = 31.897669361644866,
                lng = 34.813842901551524
            });
            context.Branches.AddOrUpdate(new Models.Branch()
            {
                Id = 3,
                Address = "Ganei Tikva",
                lat = 32.06244122293621,
                lng = 34.87091187713511
            });
        }
    }
}
