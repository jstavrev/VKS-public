using OnlineMovieStore.Models.Models;
using OnlineMovieStore.Services.Exceptions;
using OnlineMovieStore.Services.Services.Contracts;
using OnlineMovieStore.Web.Data;
using System;
using System.Collections.Generic;
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

        public IEnumerable<Genre> GetGenresPerPage(int page = 1, int pageSize = 10)
        {
            return this.context.Genres.OrderByDescending(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public IEnumerable<Genre> GetAll()
        {
            return this.context.Genres.ToList();
        }

        public int Total()
        {
            return this.context.Genres.Count();
        }

        public int TotalContainingText(string searchText)
        {
            return this.context.Genres.Where(a => a.Name.Contains(searchText)).ToList().Count();
        }

        public IEnumerable<Genre> ListByContainingText(string searchText, int page = 1, int pageSize = 10)
        {
            return this.context.Genres.Where(m => m.Name.Contains(searchText, StringComparison.InvariantCultureIgnoreCase)).OrderByDescending(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}
