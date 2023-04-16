using AutoMapper;
using Mango.Services.CartAPI.DbContexts;
using Mango.Services.CartAPI.Models;
using Mango.Services.CartAPI.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.CartAPI.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;
        private IMapper _mapper;

        public CartRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> ClearCart(string userId)
        {
            var cartHeaderInDb = await _context.CartHeaders.FirstOrDefaultAsync(x => x.UserId == userId);

            if (cartHeaderInDb != null)
            {
                _context.CartDetails
                    .RemoveRange(_context.CartDetails.Where(x => x.CartHeaderId == cartHeaderInDb.CartHeaderId));
                _context.CartHeaders.Remove(cartHeaderInDb);

                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<CartDto> CreateUpdateCart(CartDto cartDto)
        {
            var cart = _mapper.Map<Cart>(cartDto);

            // check if product exists in database, if not create it!
            var prodInDb = await _context.Products.AsNoTracking()
                .FirstOrDefaultAsync(prod => 
                prod.ProductId == cartDto.CartDetails.FirstOrDefault().ProductId);

            if (prodInDb == null)
            {
                _context.Products.Add(cart.CartDetails.FirstOrDefault().Product);
                await _context.SaveChangesAsync();
            }

            // check if header is null
            var cartHeaderInDb = await _context.CartHeaders.AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserId == cart.CartHeader.UserId);

            if (cartHeaderInDb == null)
            {
                // create header and details
                _context.CartHeaders.Add(cart.CartHeader);
                await _context.SaveChangesAsync();

                cart.CartDetails.FirstOrDefault().CartHeaderId = cart.CartHeader.CartHeaderId;
                cart.CartDetails.FirstOrDefault().Product = null;
                _context.Add(cart.CartDetails.FirstOrDefault());
                await _context.SaveChangesAsync();
            }
            else // if header is not null
            {
                // check if details has same product
                var cartDetailsInDb = await _context.CartDetails.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.ProductId == cart.CartDetails.FirstOrDefault().ProductId &&
                         x.CartHeaderId == cartHeaderInDb.CartHeaderId);

                if (cartDetailsInDb == null)
                {
                    // create details
                    cart.CartDetails.FirstOrDefault().CartHeaderId = cartHeaderInDb.CartHeaderId;
                    cart.CartDetails.FirstOrDefault().Product = null;
                    _context.CartDetails.Add(cart.CartDetails.FirstOrDefault());
                    await _context.SaveChangesAsync();
                }
                else
                {
                    // if it has details then update the count/cart details
                    cart.CartDetails.FirstOrDefault().Product = null;
                    cart.CartDetails.FirstOrDefault().Count += cartDetailsInDb.Count;
                    cart.CartDetails.FirstOrDefault().CartDetailId = cartDetailsInDb.CartDetailId;
                    cart.CartDetails.FirstOrDefault().CartHeaderId = cartDetailsInDb.CartHeaderId;
                    _context.CartDetails.Update(cart.CartDetails.FirstOrDefault());
                    await _context.SaveChangesAsync();
                }
            }

            return _mapper.Map<CartDto>(cart);
        }

        public async Task<CartDto> GetCartByUserId(string userId)
        {
            Cart cart = new()
            {
                CartHeader = await _context.CartHeaders.FirstOrDefaultAsync(x => x.UserId == userId)
            };

            cart.CartDetails = _context.CartDetails
                .Where(x => x.CartHeaderId == cart.CartHeader.CartHeaderId)
                    .Include(x => x.Product);

            return _mapper.Map<CartDto>(cart);
        }

        public async Task<bool> RemoveFromCart(int cartDetailsId)
        {
            try
            {
                CartDetails cartDetails = await _context.CartDetails
                    .FirstOrDefaultAsync(x => x.CartDetailId == cartDetailsId);

                int totalCountOfCartItems = _context.CartDetails
                    .Where(x => x.CartHeaderId == cartDetails.CartHeaderId).Count();

                _context.CartDetails.Remove(cartDetails);

                if (totalCountOfCartItems == 1)
                {
                    var cartHeaderToRemove = await _context.CartHeaders
                        .FirstOrDefaultAsync(x => x.CartHeaderId == cartDetails.CartHeaderId);

                    _context.CartHeaders.Remove(cartHeaderToRemove);
                }

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                // ToDo: log error
                return false;
            }
        }
    }
}
