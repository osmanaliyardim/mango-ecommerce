using Mango.MessageBus;
using Mango.Services.CartAPI.Messages;
using Mango.Services.CartAPI.Models;
using Mango.Services.CartAPI.Models.Dto;
using Mango.Services.CartAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CartAPI.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMessageBus _messageBus;
        protected ResponseDto _responseDto;

        public CartController(ICartRepository cartRepository, IMessageBus messageBus)
        {
            _cartRepository = cartRepository;
            _messageBus = messageBus;
            _responseDto = new ResponseDto();
        }

        [HttpGet("GetCart/{userId}")]
        public async Task<object> GetCart(string userId)
        {
            try
            {
                var cartDto = await _cartRepository.GetCartByUserId(userId);
                _responseDto.Result = cartDto;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.ErrorMessages = new List<string>()
                {
                    ex.ToString()
                };
            }

            return _responseDto;
        }

        [HttpPost("AddCart")]
        public async Task<object> AddCart(CartDto cartDto)
        {
            try
            {
                var cart = await _cartRepository.CreateUpdateCart(cartDto);
                _responseDto.Result = cart;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.ErrorMessages = new List<string>()
                {
                    ex.ToString()
                };
            }

            return _responseDto.Result;
        }

        [HttpPost("UpdateCart")]
        public async Task<object> UpdateCart(CartDto cartDto)
        {
            try
            {
                var cart = await _cartRepository.CreateUpdateCart(cartDto);
                _responseDto.Result = cart;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.ErrorMessages = new List<string>()
                {
                    ex.ToString()
                };
            }

            return _responseDto;
        }

        [HttpPost("RemoveCart")]
        public async Task<object> RemoveCart([FromBody] int cartId)
        {
            try
            {
                bool isSuccess = await _cartRepository.RemoveFromCart(cartId);
                _responseDto.Result = isSuccess;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.ErrorMessages = new List<string>()
                {
                    ex.ToString()
                };
            }

            return _responseDto;
        }

        [HttpPost("ClearCart/{userId}")]
        public async Task<object> ClearCart(string userId)
        {
            try
            {
                bool isSuccess = await _cartRepository.ClearCart(userId);
                _responseDto.Result = isSuccess;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.ErrorMessages = new List<string>()
                {
                    ex.ToString()
                };
            }

            return _responseDto;
        }

        [HttpPost("ApplyCoupon")]
        public async Task<object> ApplyCoupon([FromBody] CartDto cartDto)
        {
            try
            {
                bool isSuccess = await _cartRepository
                    .ApplyCoupon(cartDto.CartHeader.UserId, cartDto.CartHeader.CouponCode);

                _responseDto.Result = isSuccess;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.ErrorMessages = new List<string>()
                {
                    ex.ToString()
                };
            }

            return _responseDto;
        }

        [HttpPost("RemoveCoupon")]
        public async Task<object> RemoveCoupon([FromBody] string userId)
        {
            try
            {
                bool isSuccess = await _cartRepository.RemoveCoupon(userId);
                _responseDto.Result = isSuccess;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.ErrorMessages = new List<string>()
                {
                    ex.ToString()
                };
            }

            return _responseDto;
        }

        [HttpPost("Checkout")]
        public async Task<object> Checkout(CheckoutHeaderDto checkoutHeaderDto)
        {
            try
            {
                CartDto cartDto = await _cartRepository.GetCartByUserId(checkoutHeaderDto.UserId);

                if (cartDto == null)
                {
                    return BadRequest();
                }

                checkoutHeaderDto.CartDetails = cartDto.CartDetails;

                await _messageBus.PublishMessage(checkoutHeaderDto, "checkoutmessagetopic");
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.ErrorMessages = new List<string>()
                {
                    ex.ToString()
                };
            }

            return _responseDto;
        }
    }
}
