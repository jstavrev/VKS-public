using OnlineMovieStore.Models.Models;
using System;
using System.Collections.Generic;

namespace OnlineMovieStore.Services.Contracts
{
    public interface IMoviesService
    {
        Movie AddMovie(string image, string title, short year, List<Genre> genres, int actorId, double price, string description, string trailerUrl);

        IEnumerable<Movie> ListAllMovies(int page = 1, int pageSize = 10);

        IEnumerable<Movie> ListMovies();

        IEnumerable<Movie> ListMoviesByHigherPrice(int count);

        IEnumerable<Movie> ListByContainingText(string searchText, int page = 1, int pageSize = 10);

        IEnumerable<Movie> ListMoviesByActor(string firstName, string lastName);

        Movie ListMoviesByTitle(string name);

        IEnumerable<Movie> ListMoviesByYear(short year);

        Movie UpdateMoviePrice(string title, double price);

        Movie DeleteMovie(string title);

        string BuyMovie(string movieTitle);

        IEnumerable<Movie> ListMyMovies();

        int Total();
        int TotalContainingText(string searchText);
    }
}
