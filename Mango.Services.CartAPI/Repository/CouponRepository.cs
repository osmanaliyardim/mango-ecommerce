using Mango.Services.CartAPI.Models.Dto;
using Newtonsoft.Json;

namespace Mango.Services.CartAPI.Repository
{
    public class CouponRepository : ICouponRepository
    {
        private readonly HttpClient _client; // tech debt

        public CouponRepository(HttpClient client)
        {
            _client = client;
        }

        public async Task<CouponDto> GetCoupon(string couponName)
        {
            var responseJson = await _client.GetAsync($"/api/coupon/{couponName}");
            var apiContent = await responseJson.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<ResponseDto>(apiContent);

            if (response.IsSuccess)
            {
                return JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(response.Result));
            }

            return new CouponDto();
        }
    }
}
