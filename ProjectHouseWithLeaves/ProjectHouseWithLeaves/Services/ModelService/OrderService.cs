using ProjectHouseWithLeaves.Dtos;
﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectHouseWithLeaves.Models;

namespace ProjectHouseWithLeaves.Services.ModelService
{
    public class OrderService : IOrderService
    {
        private readonly MiniPlantStoreContext _context;
        private readonly ILogger<OrderService> _logger;
        private readonly IMapper _mapper;
        public OrderService(MiniPlantStoreContext context, ILogger<OrderService> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<bool> CreateOrder(OrderDtos model)
        {

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var order = new Order
                    {
                        UserId = model.UserId,
                        OrderDate = DateTime.Now,
                        SubtotalAmount = model.TotalAmount - model.Shipping.ShippingCost,
                        ShippingCostApplied = model.Shipping.ShippingCost,
                        TotalAmount = model.TotalAmount,
                        ShippingAddress = model.AddressDetail.AddressDetail,
                        Ward = model.AddressDetail.Ward,
                        District = model.AddressDetail.District,
                        City = model.AddressDetail.Province,
                        PaymentMethodId = model.PaymentMethodId,
                        ShippingMethodId = model.Shipping.ShippingMethodId,
                        Status = "PENDING"
                    };
                    _context.Orders.Add(order);
                    await _context.SaveChangesAsync();

                    //create orderItem
                    foreach (var item in model.Items)
                    {
                        var orderItem = new OrderDetail
                        {
                            OrderId = order.OrderId,
                            ProductId = item.ProductId,
                            Quantity = item.Quantity,
                            UnitPrice = item.UnitPrice
                        };
                        _context.OrderDetails.Add(orderItem);
                    }
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return false;
                }
            }
        }
        public async Task<List<Order>> GetAllOrders()
        {
            try
            {
                return await _context.Orders
                    .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all orders");
                throw;
            }
        }

        public async Task<IEnumerable<OrderHistoryDtos>> GetOrderById(int id)
        {
            var order = await _context.Orders
                                .Include(o => o.OrderDetails)
                                .ThenInclude(od => od.Product)
                                .Include(s => s.ShippingMethod)
                                .Include(p => p.PaymentMethod)
                                .Where(x => x.UserId == id)
                                .ToListAsync();
            var orderHistoryList = _mapper.Map<List<OrderHistoryDtos>>(order);
            return orderHistoryList;
        }
    }
}
