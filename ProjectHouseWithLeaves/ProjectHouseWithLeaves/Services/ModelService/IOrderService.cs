using ProjectHouseWithLeaves.Dtos;
﻿using ProjectHouseWithLeaves.Models;

namespace ProjectHouseWithLeaves.Services.ModelService
{
    public interface IOrderService
    {
        Task<bool> CreateOrder(OrderDtos order);
        
        Task<List<Order>> GetAllOrders();

        Task<Order> GetOrderrById(int id);

        Task<Order> GetOrderWithDetails(int id);

        Task<bool> UpdateOrderStatus(int orderId, string status);

        Task<object> GetOrderDataForAdmin(int id);

         Task<IEnumerable<OrderHistoryDtos>> GetOrderById(int id);
    }
}
