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
        }
    }
}
