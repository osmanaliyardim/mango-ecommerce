using System.ComponentModel.DataAnnotations;

namespace Mango.Services.CouponAPI.Models.Dto
{
    public class CouponDto
    {
        public int CouponId { get; set; }

        public string CouponCode { get; set; }

        public double CouponAmount { get; set; }
    }
}
