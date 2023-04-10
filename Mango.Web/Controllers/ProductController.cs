using Mango.Web.Models;
using Mango.Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            List<ProductDto> products = new();

            var response = await _productService.GetAllProductsAsync<ResponseDto>();

            if (response != null && response.IsSuccess)
            {
                products = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
            }

            return View(products);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                var response = await _productService.CreateProductAsync<ResponseDto>(productDto);

                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(productDto);
        }

        public async Task<IActionResult> Update(int productId)
        {
            var response = await _productService.GetAllProductByIdAsync<ResponseDto>(productId);

            if (response != null && response.IsSuccess)
            {
                ProductDto productToUpdate = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));

                return View(productToUpdate);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                var response = await _productService.UpdateProductAsync<ResponseDto>(productDto);

                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(productDto);
        }

        public async Task<IActionResult> Delete(int productId)
        {
            var response = await _productService.GetAllProductByIdAsync<ResponseDto>(productId);

            if (response != null && response.IsSuccess)
            {
                ProductDto productToDelete = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));

                return View(productToDelete);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                var response = await _productService.DeleteProductAsync<ResponseDto>(productDto.ProductId);

                if (response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(productDto);
        }
    }
}
