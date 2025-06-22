using ProjectHouseWithLeaves.Dtos;
﻿using ProjectHouseWithLeaves.Models;

namespace ProjectHouseWithLeaves.Services.ModelService
{
    public interface IOrderService
    {
        Task<bool> CreateOrder(OrderDtos order);


        /// Retrieves all orders.
        /// <returns>A task that represents the asynchronous operation, containing a list of orders.</returns>
        Task<List<Order>> GetAllOrders();

        /// Retrieves an order by its ID.
        /// <param name="id">The ID of the order to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation, containing the order if found, otherwise null.</returns>
        Task<IEnumerable<OrderHistoryDtos>> GetOrderById(int id);
    }
}
