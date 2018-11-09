using Microsoft.EntityFrameworkCore;
using OnlineMovieStore.Models.Models;
using OnlineMovieStore.Services.Contracts;
using OnlineMovieStore.Services.Exceptions;
using OnlineMovieStore.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineMovieStore.Services.Services
{
    public class MoviesService : IMoviesService
    {
        private readonly ApplicationDbContext context;

        public MoviesService(ApplicationDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Movie AddMovie(string image, string title, short year, List<Genre> genres, int actorId, double price, string description, string trailerUrl)
        {
            if (image == null)
            {
                throw new ArgumentNullException("Image cannot be null!");
            }

            if (title == null)
            {
                throw new ArgumentNullException("Title cannot be null!");
            }

            if (title.Length > 40)
            {
                throw new ArgumentOutOfRangeException("Title lenght cannot be more than 40 symbols!");
            }

            if(description == null)
            {
                throw new ArgumentNullException("Description cannot be null");
            }

            if(description.Length > 350)
            {
                throw new ArgumentOutOfRangeException("Description cannot be more than 350 symbols");
            }

            if(trailerUrl == null)
            {
                throw new ArgumentNullException("Trailer Url cannot be null");
            }

            var movie = this.context.Movies
              .FirstOrDefault(m => m.Title == title);

            if (movie != null)
            {
                throw new EntityNotFoundException($"Movie with title {title} already exists!");
            }

            movie = new Movie
            {
                Title = title,
                Year = year,
                Price = price,
                ActorId = actorId,
                Image = image,
                Description = description,
                TrailerURL = trailerUrl
            };

            this.context.Movies.Add(movie);

            foreach (var g in genres)
            {
                this.context.GenreMovies.Add(new GenreMovie
                {
                    MovieId = movie.Id,
                    GenreId = g.Id
                });
            }
            this.context.SaveChanges();

            return movie;
        }

        public Movie DeleteMovie(string title)
        {
            if (title == null)
            {
                throw new ArgumentNullException("Title cannot be null!");
            }

            if (title.Length > 40)
            {
                throw new ArgumentOutOfRangeException("Title lenght cannot be more than 40 symbols!");
            }

            var movie = this.context.Movies
                .FirstOrDefault(m => m.Title == title);

            if (movie == null)
            {
                throw new EntityNotFoundException($"Movie with title {title} does not exist!");
            }

            movie.IsDeleted = true;

            this.context.SaveChanges();

            return movie;
        }

        public IEnumerable<Movie> ListAllMovies(int page = 1, int pageSize = 10)
        {
            return this.context.Movies.OrderByDescending(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public IEnumerable<Movie> ListMovies()
        {
            var moviesQuery = this.context.Movies
                .AsQueryable();

            moviesQuery = moviesQuery
                .Include(m => m.Actor)
                .Include(m => m.Genres)
                    .ThenInclude(mg => mg.Genre);
            
            var movies = moviesQuery.ToList();

            if (movies != null && movies.Count != 0)
            {
                return movies;
            }
            else
            {
                throw new EntityNotFoundException("Cannot find movies with this filter!");
            }
        }

        public IEnumerable<Movie> ListMoviesByHigherPrice(int count)
        {
            if(count < 1)
            {
                throw new ArgumentOutOfRangeException("Count must be more than 0!");
            }
            var moviesQuery = this.context.Movies
                .AsQueryable();

            moviesQuery = moviesQuery
                .Include(m => m.Actor)
                .Include(m => m.Genres)
                    .ThenInclude(mg => mg.Genre)
                .OrderByDescending(m => m.Price)
                .Take(count);

            var movies = moviesQuery.ToList();

            if (movies != null && movies.Count != 0)
            {
                return movies;
            }
            else
            {
                throw new EntityNotFoundException("Cannot find movies with this filter!");
            }
        }

        public IEnumerable<Movie> ListMoviesByActor(string firstName, string lastName)
        {
            if (firstName == null)
            {
                throw new ArgumentNullException("Firt name cannot be null!");
            }

            if (firstName.Length > 25)
            {
                throw new ArgumentOutOfRangeException("Fist name cannot be longer than 25 symbols!");
            }

            if (lastName == null)
            {
                throw new ArgumentNullException("Last name cannot be null!");
            }

            if (lastName.Length > 25)
            {
                throw new ArgumentOutOfRangeException("Last name cannot be longer than 25 symbols!");
            }

            var moviesQuery = this.context.Movies
                .AsQueryable();

            moviesQuery = moviesQuery
                 .Include(m => m.Actor)
                 .Include(m => m.Genres)
                     .ThenInclude(mg => mg.Genre)
                 .Where(m => m.Actor.FirstName == firstName && m.Actor.LastName == lastName);

            var movies = moviesQuery.ToList();

            if (movies != null && movies.Count != 0)
            {
                return movies;
            }
            else
            {
                throw new EntityNotFoundException("Cannot find movies with this filter!");
            }
        }

        public Movie ListMoviesByTitle(string title)
        {
            if (title == null)
            {
                throw new ArgumentNullException("Title cannot be null");
            }

            if (title.Length > 40)
            {
                throw new ArgumentOutOfRangeException("Title length cannot be more than 40 symbols");
            }

            var moviesQuery = this.context.Movies
                .AsQueryable();

            moviesQuery = moviesQuery
               .Include(m => m.Actor)
               .Include(m => m.Genres)
                   .ThenInclude(mg => mg.Genre)
               .Where(m => m.Title == title);

            var movie = moviesQuery.ToList()[0];

            if (movie != null)
            {
                return movie;
            }
            else
            {
                throw new EntityNotFoundException("Cannot find movies with this filter!");
            }
        }

        public IEnumerable<Movie> ListMoviesByYear(short year)
        {
            if (year < 1800 || year > 9999)
            {
                throw new ArgumentOutOfRangeException("Year must be in between 1800 and 9999");
            }

            var moviesQuery = this.context.Movies
                .AsQueryable();

            moviesQuery = moviesQuery
               .Include(m => m.Actor)
               .Include(m => m.Genres)
                   .ThenInclude(mg => mg.Genre)
               .Where(m => m.Year == year);

            var movies = moviesQuery.ToList();

            if (movies != null && movies.Count != 0)
            {
                return movies;
            }
            else
            {
                throw new EntityNotFoundException("Cannot find movies with this filter!");
            }
        }

        public Movie UpdateMoviePrice(string title, double price)
        {
            if (title == null)
            {
                throw new ArgumentNullException("Title cannot be null!");
            }

            if (title.Length > 40)
            {
                throw new ArgumentOutOfRangeException("Title lenght cannot be more than 40 symbols!");
            }

            if (price < 1 || price > 200)
            {
                throw new ArgumentOutOfRangeException("Price must be between 1 and 200$!");
            }

            var movie = this.context.Movies
                .FirstOrDefault(m => m.Title == title);

            if (movie == null)
            {
                throw new EntityNotFoundException($"Movie with title {title} does not exist!");
            }

            if (price == movie.Price)
            {
                throw new ArgumentException($"{title} price is already {price:F2}$!");
            }

            movie.Price = price;

            this.context.SaveChanges();

            return movie;
        }

        //TODO: NEEDS USER INFO
        public string BuyMovie(string movieTitle)
        {
            if (movieTitle == null)
            {
                throw new ArgumentNullException("Title cannot be null!");
            }

            var movie = this.context.Movies
                .FirstOrDefault(m => m.Title == movieTitle);

            if (movie == null)
            {
                throw new EntityNotFoundException($"Movie with title {movieTitle} already exists!");
            }

            /*  var user = this.unitOfWork.GetRepo<User>().All()
                  .FirstOrDefault(u => u.Username == this.userSession.user.Username);

              if (user.Balance < movie.Price)
              {
                  throw new ArgumentException("You don't have enough money to buy this movie!");
              }

              this.context.WatchedMovies.Add(new WatchedMovies
              {
                  UserId = user.Id,
                  MovieId = movie.Id
              });

              this.context.Orders.Add(new Order
              {
                  UserId = user.Id,
                  MovieId = movie.Id,
                  Date = DateTime.Now
              });

              user.Balance -= movie.Price;

              this.context.SaveChanges();
              */
            return "You have succesfully added movie to your collection.";
        }

        //TODO: NEEDS USER INFO2
        public IEnumerable<Movie> ListMyMovies()
        {
            /*var user = this.unitOfWork.GetRepo<User>().All()
                .FirstOrDefault(u => u.Username == this.userSession.user.Username);

            var watchedMovies = this.context.WatchedMovies
                .Where(u => u.UserId == user.Id)
                .Select(m => m.Movie)
                .Include(m => m.Genres)
                    .ThenInclude(mg => mg.Genre)
                .Include(m => m.Actor)
                .ToList();

            if (watchedMovies != null && watchedMovies.Count != 0)
            {
                return watchedMovies;
            }
            else
            {
                throw new EntityNotFoundException("Cannot find movies with this filter!");
            }*/
            return new List<Movie>();
        }

        public int Total()
        {
            return this.context.Movies.Count();
        }

        public IEnumerable<Movie> ListByContainingText(string searchText, int page = 1, int pageSize = 10)
        {
            return this.context.Movies.Where(m => m.Title.Contains(searchText)).OrderByDescending(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public int TotalContainingText(string searchText)
        {
            return this.context.Movies.Where(m => m.Title.Contains(searchText)).ToList().Count();
        }
    }
}