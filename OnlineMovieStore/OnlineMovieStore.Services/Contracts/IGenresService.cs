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
    }
}
