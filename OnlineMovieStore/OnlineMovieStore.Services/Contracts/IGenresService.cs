using OnlineMovieStore.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMovieStore.Services.Services.Contracts
{
    public interface IGenresService
    {
        Genre AddGenre(string title);
        Genre DeleteGenre(string title);
        IEnumerable<Genre> GetAll();
        int Total();
        int TotalContainingText(string searchText);
        IEnumerable<Genre> GetGenresPerPage(int page, int pageSize);
        IEnumerable<Genre> ListByContainingText(string searchText, int page, int pageSize);
    }
}
