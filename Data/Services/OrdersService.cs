using Kitaab.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kitaab.Data.Services
{
    public class OrdersService : IOrderService
    {
        private readonly AppDBContext _context;
        public OrdersService(AppDBContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetOrderByUserIdAndRoleAsync(string userId, string userRole)
        {
            var orders = await _context.Orders.Include(n => n.OrderItems).ThenInclude(n => n.Book).Include(n => n.User).ToListAsync();
            if (userRole != "Admin")
            {
                orders = orders.Where(n => n.UserId == userId).ToList();
            }
            return orders;
        }

        public async Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress)
        {
            var order = new Order()
            {
                UserId = userId,
                Email = userEmailAddress,
            };
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            foreach (var item in items)
            {
                var book = await _context.Books.FirstOrDefaultAsync(n => n.Id == item.Book.Id);
                if (book != null && book.Quantity - item.Amount >= 0)
                {                
                    book.Quantity = book.Quantity - item.Amount;
                    var orderItem = new OrderItem()
                    {
                        Amount = item.Amount,
                        BookId = item.Book.Id,
                        OrderId = order.Id,
                        Price = item.Book.Price
                    };
                    await _context.OrderItems.AddAsync(orderItem);
                }
            }
            await _context.SaveChangesAsync();
        }
    }
}
