using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineMovieStore.Models.Models;
using OnlineMovieStore.Services.Contracts;
using OnlineMovieStore.Services.Exceptions;
using OnlineMovieStore.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineMovieStore.Services
{
    public class UserService : IUsersService
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public UserService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        //TODO: GET ORDERS OF THE CURRENT USER
        public IEnumerable<Movie> Orders(string id)
        {
            var userOrders = this.context.Orders
               .Where(u => u.UserId == id)
               .Select(m => m.Movie)
               .Include(m => m.Genres)
                .ThenInclude(mg => mg.Genre)
               .Include(m => m.Actor)
               .ToList();

            if (userOrders == null)
            {
                throw new EntityNotFoundException("Can't find orders with this user information");
            }
            else
            {
                return userOrders;
            }
        }

        //TODO: Validations
        public ApplicationUser GetUser(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException("Provided search string for id is null or empty");
            }

            var userId = this.context.Users.Find(id);

            if (userId == null)
            {
                throw new EntityNotFoundException("Can't find user with this key");
            }
            else
            {
                return userId;
            }
        }

        public IEnumerable<Movie> OrdersDetails(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException("Provided search string for id is null or empty");
            }

            var userMovieDetails = this.context.Orders
               .Where(u => u.UserId == id)
               .Select(m => m.Movie)
               .ToList();

                return userMovieDetails;
        }

        public ApplicationUser AddToVallet(double amount, string userId)
        {
            if (amount < 0)
            {
                throw new ArgumentNullException("Cant add null value to Balance");
            }

            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentNullException("Provided search string for id is null or empty");
            }

            var user = this.context.Users.Find(userId);

            if (user == null)
            {
                throw new EntityNotFoundException("Can't find user with this key information");
            }

            user.Balance += amount;

            this.context.SaveChanges();

            return user;
        }

        public double GetBalance(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException("Provided search string for id is null or empty");
            }

            var user = this.context.Users.Find(id);

            if (user == null)
            {
                throw new EntityNotFoundException("Can't find user with this key information");
            }

            return user.Balance;
        }

        public ApplicationUser UpdateAccountDetails(string UserName, string Email, string PhoneNumber, string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentNullException("Provided search string for id is null or empty");
            }

            if (string.IsNullOrWhiteSpace(UserName))
            {
                throw new ArgumentNullException("Parameter UserName is null");
            }

            if (string.IsNullOrWhiteSpace(Email))
            {
                throw new ArgumentNullException("Parameter Email is null");
            }

            var user = this.context.Users.Find(userId);

            if (user == null)
            {
                throw new EntityNotFoundException("Can't find user with this key information");
            }

            user.UserName = UserName;
            user.Email = Email;
            user.PhoneNumber = PhoneNumber;
            user.NormalizedUserName = UserName.ToUpper();

            this.context.SaveChanges();

            userManager.UpdateAsync(user);

            return user;
        }

        public IEnumerable<ApplicationUser> GetAllUsers(int page = 1, int pageSize = 10)
        {
            var allUsers = this.context.Users.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return allUsers;
        }

        public int Total()
        {

            return this.context.Users.Count();
        }

        public int TotalContainingText(string searchText)
        {
            return this.context.Users.Where(u => u.UserName.Contains(searchText, StringComparison.InvariantCultureIgnoreCase)).Count();
        }

        public IEnumerable<ApplicationUser> UsersContainingText(string searchText, int page = 1, int pageSize = 10)
        {
            var users = this.context.Users.Where(u => u.UserName.Contains(searchText, StringComparison.InvariantCultureIgnoreCase)).Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return users;
        }
    }
}