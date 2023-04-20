using AutoMapper;
using Mango.Services.OrderAPI.DbContexts;
using Mango.Services.OrderAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.OrderAPI.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DbContextOptions<ApplicationDbContext> _context;

        public OrderRepository(DbContextOptions<ApplicationDbContext> context)
        {
            _context = context;
        }

        public async Task<bool> AddOrder(OrderHeader orderHeader)
        {
            await using var _db = new ApplicationDbContext(_context);

            _db.OrderHeaders.Add(orderHeader);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task UpdateOrderPaymentStatus(int orderHeaderId, bool isPaid)
        {
            await using var _db = new ApplicationDbContext(_context);

            var orderHeaderFromDb = await _db.OrderHeaders.FirstOrDefaultAsync(u => u.OrderHeaderId == orderHeaderId);

            if (orderHeaderFromDb != null)
            {
                orderHeaderFromDb.PaymentStatus = isPaid;
                await _db.SaveChangesAsync();
            }
        }
    }
}
