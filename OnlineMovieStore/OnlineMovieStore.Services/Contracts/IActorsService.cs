using OnlineMovieStore.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMovieStore.Services.Services.Contracts
{
    public interface IActorsService
    {
        Actor AddActor(string firstName, string lastName, int age);
        Actor UpdateActorAge(string firstName, string lastName, int age);
        Actor DeleteActor(string firstName, string lastName);
        IEnumerable<Actor> GetAll();
        int Total();
        IEnumerable<Actor> GetActorsPerPage(int page, int pageSize);
        IEnumerable<Actor> ListByContainingText(string searchText, int page, int pageSize);
        int TotalContainingText(string searchText);
    }
}
