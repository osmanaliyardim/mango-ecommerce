using Mango.Web.Models;

namespace Mango.Web.Services.IServices
{
    public interface IProductService : IBaseService
    {
        Task<T> GetAllProductsAsync<T>();

        Task<T> GetAllProductByIdAsync<T>(int productId);

        Task<T> CreateProductAsync<T>(ProductDto productDto);

        Task<T> UpdateProductAsync<T>(ProductDto productDto);

        Task<T> DeleteProductAsync<T>(int productId);
    }
}
