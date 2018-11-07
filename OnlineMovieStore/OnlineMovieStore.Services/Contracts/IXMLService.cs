using OnlineMovieStore.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMovieStore.Services.Services.Contracts
{
    public interface IXMLService
    {
        string ImportActors(string path);
        string ImportMovies(string path);
    }
}
