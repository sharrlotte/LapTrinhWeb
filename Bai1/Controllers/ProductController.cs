namespace Bai1.Controllers
{
	using Bai1.Models;
	using Bai1.Repository;
	using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class ProductController : Controller

    {

        private readonly IProductRepository _productRepository;

        private readonly ICategoryRepository _categoryRepository;



        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)

        {
            _productRepository = productRepository;

            _categoryRepository = categoryRepository;
        }



        public IActionResult Add()

        {

            var categories = _categoryRepository.GetAllCategories();

            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            return View();

        }



		[HttpPost]
        // Thêm [FromForm] để tách file ra khỏi Model
		public async Task<IActionResult> Add([FromForm] Product product, IFormFile imageUrl, List<IFormFile> imageUrls)

		{

			if (ModelState.IsValid)

			{
                try
                {


				if (imageUrl != null)

				{
					product.ImageUrl = await SaveImage(imageUrl);
				}

                // Sử tên biến lại cho đúng
				if (imageUrls != null)

				{

					product.ImageUrls = new List<string>();

					foreach (var file in imageUrls)
					{
						product.ImageUrls.Add(await SaveImage(file));
					}

				}



				_productRepository.Add(product);

				return RedirectToAction("Index");

                } catch(Exception ex)
                {

                }
            }



			return View(product);

		}



		private async Task<string> SaveImage(IFormFile image)

		{
            if (!image.FileName.EndsWith("png"))
            {
                throw new Exception("File is not image file");
            }

			var savePath = Path.Combine("wwwroot/images", image.FileName); // Thay đổi đường dẫn theo cấu hình của bạn 

			using (var fileStream = new FileStream(savePath, FileMode.Create))

			{

				await image.CopyToAsync(fileStream);

			}

			return "/images/" + image.FileName; // Trả về đường dẫn tương đối 

		}



		public IActionResult Index()

        {

            var products = _productRepository.GetAll();

            return View(products);

        }



        public IActionResult Display(int id)

        {

            var product = _productRepository.GetById(id);

            if (product == null)

            {

                return NotFound();

            }

            return View(product);

        }



        public IActionResult Update(int id)

        {

            var product = _productRepository.GetById(id);

            if (product == null)

            {

                return NotFound();

            }

            return View(product);

        }



        [HttpPost]

        public IActionResult Update(Product product)

        {

            if (ModelState.IsValid)

            {

                _productRepository.Update(product);

                return RedirectToAction("Index");

            }

            return View(product);

        }


        public IActionResult Delete(int id)

        {

            var product = _productRepository.GetById(id);

            if (product == null)

            {

                return NotFound();

            }

            return View(product);

        }


        // Sửa ActionName("Delete") thành ActionName("Delete") để trùng với DeleteConfirm bên Delete.cshtml 
        [HttpPost, ActionName("DeleteConfirmed")]
        public IActionResult DeleteConfirmed(int id)
        {
            _productRepository.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
