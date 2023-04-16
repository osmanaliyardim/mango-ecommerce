using AutoMapper;
using Mango.Services.CouponAPI.DbContexts;
using Mango.Services.CouponAPI.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.CouponAPI.Repositories
{
    public class CouponRepository : ICouponRepository
    {
        private readonly ApplicationDbContext _context;
        protected IMapper _mapper;

        public CouponRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CouponDto> GetCouponByCode(string couponCode)
        {
            var couponFromDb = await _context.Coupons.FirstOrDefaultAsync(coupon => coupon.CouponCode == couponCode);

            return _mapper.Map<CouponDto>(couponFromDb);
        }
    }
}
