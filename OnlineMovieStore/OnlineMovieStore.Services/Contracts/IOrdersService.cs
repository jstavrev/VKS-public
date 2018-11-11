using OnlineMovieStore.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMovieStore.Services.Services.Contracts
{
    public interface IOrdersService
    {
        List<Order> ListAllOrders();
        List<Order> ListByPage(int page = 1, int pageSize = 10);
        List<Order> ListOrdersContainingText(string searchText, int page = 1, int pageSize = 10);
        int TotalOrders();
        int TotalContainingText(string searchText);
    }
}
