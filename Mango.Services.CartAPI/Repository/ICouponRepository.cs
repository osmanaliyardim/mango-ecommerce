using Mango.Services.CartAPI.Models.Dto;

namespace Mango.Services.CartAPI.Repository
{
    public interface ICouponRepository
    {
        Task<CouponDto> GetCoupon(string couponName);
    }
}