namespace Bai2.Areas.Admin.Controllers
{
    using Bai2.Repository;
    using Microsoft.AspNetCore.Mvc;
    
    public class ProductController : Controller

    {

        private readonly IProductRepository _productRepository;

        private readonly ICategoryRepository _categoryRepository;



        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)

        {
            _productRepository = productRepository;

            _categoryRepository = categoryRepository;
        }



        public async Task<IActionResult> Index()

        {

            var products = await _productRepository.GetAllAsync();

            return View(products);

        }



        public async Task<IActionResult> Display(int id)

        {

            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)

            {

                return NotFound();

            }

            return View(product);

        }
    }
}
