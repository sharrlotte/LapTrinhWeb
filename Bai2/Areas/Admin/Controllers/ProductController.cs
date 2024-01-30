namespace Bai2.Controllers
{
    using Bai2.Models;
    using Bai2.Repository;
    using Bai2.Request;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class ProductController : Controller

    {

        private readonly IProductRepository _productRepository;

        private readonly ICategoryRepository _categoryRepository;



        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)

        {
            _productRepository = productRepository;

            _categoryRepository = categoryRepository;
        }



		public async Task<IActionResult> Add()

		{
			var categories = await _categoryRepository.GetAllAsync();

			ViewBag.Categories = new SelectList(categories, "Id", "Name");

			return View("Add");
		}



		[HttpPost]

		public async Task<IActionResult> Add([FromForm] CreateProductRequest request)

		{

			if (ModelState.IsValid)

			{

                Product product = new Product();
                product.Name = request.Name;
                product.Description = request.Description;
                product.Price = request.Price;
                product.CategoryId = request.CategoryId;

				if (request.Image != null)

				{
					product.ImageUrl = await SaveImage(request.Image);
				}
/*
				if (imageUrls != null)

				{

					product.ImageUrls = new List<string>();

					foreach (var file in imageUrls)
					{
						product.ImageUrls.Add(await SaveImage(file));
					}

				}*/



				await _productRepository.AddAsync(product);

				return RedirectToAction(nameof(Index));

			}

            var categories = await _categoryRepository.GetAllAsync();

            ViewBag.Categories = new SelectList(categories, "Id", "Name");



            return View(request);

		}



		private async Task<string> SaveImage(IFormFile image)

		{

            if (!image.FileName.EndsWith("png"))
            {
                throw new Exception("Invalid image format");
            }

            var uuid = Guid.NewGuid().ToString() + ".png";

			var savePath = Path.Combine("wwwroot/images", uuid); // Thay đổi đường dẫn theo cấu hình của bạn 

			using (var fileStream = new FileStream(savePath, FileMode.Create))

			{

				await image.CopyToAsync(fileStream);

			}

			return "/images/" + uuid; // Trả về đường dẫn tương đối 

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

            return View("Display", product);

        }



        public async Task<IActionResult> Update(int id)

        {

            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)

            {

                return NotFound();

            }

            var categories = await _categoryRepository.GetAllAsync();

            ViewBag.Categories = new SelectList(categories, "Id", "Name");


            return View(new UpdateProductRequest
            {
                Id= product.Id,
                CategoryId = product.CategoryId,
                Description = product.Description,
                Name = product.Name,
                Price = product.Price,
            });;

        }



        [HttpPost]

        public async Task<IActionResult> Update(UpdateProductRequest request)

        {

            if (ModelState.IsValid)

            {
                var product = await _productRepository.GetByIdAsync(request.Id);

                if (product == null)
                {
                    return NotFound();
                }

                product.Price = request.Price;
                product.Description = request.Description;
                product.Name = request.Name;
                product.CategoryId= request.CategoryId;

                await _productRepository.UpdateAsync(product);

                return RedirectToAction(nameof(Index));

            }

            var categories = await _categoryRepository.GetAllAsync();

            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            return View(request);
        }


        public async Task<IActionResult> Delete(int id)

        {

            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)

            {

                return NotFound();

            }

            return View(product);

        }



        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productRepository.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
