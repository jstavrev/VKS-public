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
    }
}
