<<<<<<< Updated upstream
﻿using ProjectHouseWithLeaves.Dtos;
=======
﻿using ProjectHouseWithLeaves.Models;
>>>>>>> Stashed changes

namespace ProjectHouseWithLeaves.Services.ModelService
{
    public interface IOrderService
    {
<<<<<<< Updated upstream
        Task<bool> CreateOrder(OrderDtos order);
=======
        /// Creates a new order.
        /// <param name="order">The order to create.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task CreateOrder(Order order);

        /// Retrieves all orders.
        /// <returns>A task that represents the asynchronous operation, containing a list of orders.</returns>
        Task<List<Order>> GetAllOrders();

        /// Retrieves an order by its ID.
        /// <param name="id">The ID of the order to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation, containing the order if found, otherwise null.</returns>
        Task<Order> GetOrderById(int id);
>>>>>>> Stashed changes
    }
}
