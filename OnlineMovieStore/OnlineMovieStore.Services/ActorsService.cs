using OnlineMovieStore.Models.Models;
using OnlineMovieStore.Services.Exceptions;
using OnlineMovieStore.Services.Services.Contracts;
using OnlineMovieStore.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineMovieStore.Services.Services
{
    public class ActorsService : IActorsService
    {
        private readonly ApplicationDbContext context;

        public ActorsService(ApplicationDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Actor AddActor(string firstName, string lastName, int age)
        {
            var actor = this.context.Actors
                .FirstOrDefault(a => a.FirstName == firstName && a.LastName == lastName);

            actor = new Actor
            {
                FirstName = firstName,
                LastName = lastName,
                Age = age
            };

            this.context.Actors.Add(actor);

            this.context.SaveChanges();

            return actor;
        }

        public Actor DeleteActor(string firstName, string lastName)
        {

            if (firstName == null)
            {
                throw new ArgumentNullException("First name cannot be null!");
            }
            if (firstName.Length > 25)
            {
                throw new ArgumentOutOfRangeException("First name length cannot be more than 25 symbols!");
            }

            if (lastName == null)
            {
                throw new ArgumentNullException("Last name cannot be null!");
            }
            if (lastName.Length > 25)
            {
                throw new ArgumentOutOfRangeException("Last name length cannot be more than 25 symbols!");
            }

            var actor = this.context.Actors
                .FirstOrDefault(a => a.FirstName == firstName && a.LastName == lastName);

            if (actor == null)
            {
                throw new EntityNotFoundException($"Actor with name {firstName} {lastName} does not exist!");
            }

            if (actor.IsDeleted == false)
            {
                actor.IsDeleted = true;
            }
            else
            {
                throw new ArgumentException("Actor is already deleted!");
            }

            this.context.SaveChanges();

            return actor;
        }

        public IEnumerable<Actor> GetActorsPerPage(int page = 1, int pageSize = 10)
        {
            return this.context.Actors.OrderByDescending(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public IEnumerable<Actor> GetAll()
        {
            return this.context.Actors.ToList();
        }

        public IEnumerable<Actor> ListByContainingText(string searchText, int page, int pageSize)
        {
            var all = this.context.Actors.ToList();
            List<Actor> result = new List<Actor>();

            foreach (var actor in all)
            {
                if ($"{actor.FirstName} {actor.LastName}".Contains(searchText, StringComparison.InvariantCultureIgnoreCase))
                {
                    result.Add(actor);
                }
            }

            return result.OrderByDescending(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public int Total()
        {
            return this.context.Actors.Count();
        }

        public int TotalContainingText(string searchText)
        {
            return this.context.Actors.Where(a => $"{a.FirstName} {a.LastName}".Contains(searchText, StringComparison.InvariantCultureIgnoreCase)).ToList().Count();
        }

        public Actor UpdateActorAge(string firstName, string lastName, int age)
        {

            if (firstName == null)
            {
                throw new ArgumentNullException("First name cannot be null!");
            }
            if (firstName.Length > 25)
            {
                throw new ArgumentOutOfRangeException("First name length cannot be more than 25 symbols!");
            }

            if (lastName == null)
            {
                throw new ArgumentNullException("Last name cannot be null!");
            }
            if (lastName.Length > 25)
            {
                throw new ArgumentOutOfRangeException("Last name length cannot be more than 25 symbols!");
            }

            if (age < 6 || age > 150)
            {
                throw new ArgumentOutOfRangeException("Age must be between 6 and 150 years!");
            }

            var actor = this.context.Actors
                .FirstOrDefault(a => a.FirstName == firstName && a.LastName == lastName);

            if (actor == null)
            {
                throw new EntityNotFoundException($"Actor with name {firstName} {lastName} does not exist!");
            }

            if (actor.Age == age)
            {
                throw new ArgumentException($"{firstName} {lastName} age is already {age}!");
            }

            actor.Age = age;

            this.context.SaveChanges();

            return actor;
        }
    }
}
