using Mango.Services.CouponAPI.Models.Dto;

namespace Mango.Services.CouponAPI.Repositories
{
    public interface ICouponRepository
    {
        Task<CouponDto> GetCouponByCode(string couponCode);
    }
}
