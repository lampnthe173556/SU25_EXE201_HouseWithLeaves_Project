using ProjectHouseWithLeaves.Dtos;
﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectHouseWithLeaves.Models;

namespace ProjectHouseWithLeaves.Services.ModelService
{
    public class OrderService : IOrderService
    {
        private readonly MiniPlantStoreContext _context;

        public OrderService(MiniPlantStoreContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateOrder(OrderDtos model)
        {
            if (model == null || model.Items == null || !model.Items.Any())
            {
                return false;
            }

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
                    .Include(o => o.User)
                    .Include(o => o.PaymentMethod)
                    .Include(o => o.ShippingMethod)
                    .Include(o => o.OrderDetails)
                        .ThenInclude(od => od.Product)
                            .ThenInclude(p => p.ProductImages)
                    .OrderByDescending(o => o.OrderDate)
                    .ToListAsync();
            }
            catch (Exception)
            {
                return new List<Order>();
            }
        }

        public async Task<Order> GetOrderById(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            try
            {
                return await _context.Orders
                    .Include(o => o.User)
                    .Include(o => o.PaymentMethod)
                    .Include(o => o.ShippingMethod)
                    .Include(o => o.OrderDetails)
                        .ThenInclude(od => od.Product)
                            .ThenInclude(p => p.ProductImages)
                    .FirstOrDefaultAsync(o => o.OrderId == id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Order> GetOrderWithDetails(int id)
        {
            return await GetOrderById(id);
        }

        public async Task<bool> UpdateOrderStatus(int orderId, string status)
        {
            if (orderId <= 0 || string.IsNullOrWhiteSpace(status))
            {
                return false;
            }

            try
            {
                var order = await _context.Orders.FindAsync(orderId);
                if (order == null)
                {
                    return false;
                }

                // Convert status to proper case for database storage
                string normalizedStatus = status.ToUpper();
                
                // Validate status transition for our simplified status system
                var validTransitions = new Dictionary<string, string[]>
                {
                    ["PENDING"] = new[] { "ACCEPTED", "REJECTED" },
                    ["ACCEPTED"] = new string[0], // Cannot change once accepted
                    ["REJECTED"] = new string[0]  // Cannot change once rejected
                };

                var currentStatus = order.Status?.ToUpper() ?? "PENDING";
                
                if (validTransitions.ContainsKey(currentStatus))
                {
                    var allowedTransitions = validTransitions[currentStatus];
                    if (!allowedTransitions.Contains(normalizedStatus))
                    {
                        return false;
                    }
                }

                order.Status = normalizedStatus;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<object> GetOrderDataForAdmin(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            try
            {
                var order = await GetOrderWithDetails(id);
                
                if (order == null)
                {
                    return null;
                }

                return new
                {
                    orderId = order.OrderId,
                    orderDate = order.OrderDate,
                    subtotalAmount = order.SubtotalAmount,
                    discountAppliedAmount = order.DiscountAppliedAmount,
                    shippingCostApplied = order.ShippingCostApplied,
                    totalAmount = order.TotalAmount,
                    status = order.Status,
                    shippingAddress = order.ShippingAddress,
                    user = order.User != null ? new
                    {
                        email = order.User.Email,
                        phone = order.User.Phone
                    } : null,
                    paymentMethod = order.PaymentMethod != null ? new
                    {
                        methodName = order.PaymentMethod.MethodName
                    } : null,
                    shippingMethod = order.ShippingMethod != null ? new
                    {
                        methodName = order.ShippingMethod.MethodName
                    } : null,
                    orderDetails = order.OrderDetails.Select(od => new
                    {
                        quantity = od.Quantity,
                        unitPrice = od.UnitPrice,
                        product = od.Product != null ? new
                        {
                            productName = od.Product.ProductName,
                            mainImage = od.Product.ProductImages.FirstOrDefault(pi => pi.MainPicture == true)?.ImageUrl 
                                       ?? od.Product.ProductImages.FirstOrDefault()?.ImageUrl,
                            size = od.Product.Size
                        } : null
                    }).ToList()
                };
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
