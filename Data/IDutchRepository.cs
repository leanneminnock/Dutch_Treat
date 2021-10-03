using Dutch_Treat.Data.Entities;
using System.Collections.Generic;

namespace Dutch_Treat.Data
{
    public interface IDutchRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductsByCatagory(string catagory);


        IEnumerable<Order> GetAllOrders(bool includeItems);
        IEnumerable<Order> GetAllOrdersByUser(string username, bool includeItems);
        Order GetOrderById(string username, int id);
        bool SaveAll();
        void AddEntity(object model);
    }
}