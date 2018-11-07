using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMovieStore.Services.Services.Contracts
{
    public interface IPdfExporterService
    {
        void CreateMoviePdfReport(string fileName);
    }
}
