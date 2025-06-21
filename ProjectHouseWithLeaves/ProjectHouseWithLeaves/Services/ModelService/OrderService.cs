<<<<<<< Updated upstream
﻿
using ProjectHouseWithLeaves.Dtos;
=======
﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
>>>>>>> Stashed changes
using ProjectHouseWithLeaves.Models;

namespace ProjectHouseWithLeaves.Services.ModelService
{
    public class OrderService : IOrderService
    {
        private readonly MiniPlantStoreContext _context;
<<<<<<< Updated upstream

        public OrderService(MiniPlantStoreContext context)
        {
            _context = context;
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
=======
        private readonly ILogger<OrderService> _logger;
        private readonly IMapper _mapper;
        public OrderService(MiniPlantStoreContext context, ILogger<OrderService> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task CreateOrder(Order order)
        {
            try
            {
                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating order");
                throw;
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

        public Task<Order> GetOrderById(int id)
        {
            throw new NotImplementedException();
>>>>>>> Stashed changes
        }
    }
}
