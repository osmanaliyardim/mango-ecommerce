using Mango.Services.CartAPI.Models;

namespace Mango.Services.CartAPI.Repository
{
    public interface ICartRepository
    {
        Task<CartDto> GetCartByUserId(string userId);

        Task<CartDto> CreateUpdateCart(CartDto cartDto);

        Task<bool> RemoveFromCart(int cartDetailsId);

        Task<bool> ClearCart(string userId);
    }
}
