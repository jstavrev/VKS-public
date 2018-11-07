using OnlineMovieStore.Models.Models;
using OnlineMovieStore.Services.Exceptions;
using OnlineMovieStore.Services.Services.Contracts;
using OnlineMovieStore.Web.Data;
using System;
using System.Linq;

namespace OnlineMovieStore.Services.Services
{
    public class GenresService : IGenresService
    {
        private readonly ApplicationDbContext context;

        public GenresService(ApplicationDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Genre AddGenre(string name)
        {

            //if (name == null)
            //{
            //    throw new ArgumentNullException("Genre name cannot be null!");
            //}
            //if (name.Length > 20)
            //{
            //    throw new ArgumentOutOfRangeException("Genre name length cannot be more than 20 symbols!");
            //}

            var genre = this.context.Genres
                .FirstOrDefault(g => g.Name == name);

            if (genre != null)
            {
                throw new ArgumentException($"Genre {name} already exists!");
            }

            genre = new Genre
            {
                Name = name
            };

            this.context.Genres.Add(genre);
            this.context.SaveChanges();

            return genre;
        }

        public Genre DeleteGenre(string name)
        {
            
            var genre = this.context.Genres
                .FirstOrDefault(g => g.Name == name && g.IsDeleted == false);

            if (genre == null)
            {
                throw new EntityNotFoundException($"Genre with name {name} does not exist");
            }
           
            genre.IsDeleted = true;

            this.context.SaveChanges();

            return genre;
        }
    }
}
