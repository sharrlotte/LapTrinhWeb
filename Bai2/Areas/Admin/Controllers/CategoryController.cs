namespace Bai2.Controllers
{
    using Bai2.Models;
    using Bai2.Repository;
    using Bai2.Request;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class CategoryController : Controller

    {


        private readonly ICategoryRepository _categoryRepository;



        public CategoryController( ICategoryRepository categoryRepository)

        {
            _categoryRepository = categoryRepository;
        }



		public async Task<IActionResult> Add()

		{
			return View();
		}



		[HttpPost]

		public async Task<IActionResult> Add([FromForm] CreateCategoryRequest request)

		{

            if (ModelState.IsValid)

			{
                Category category = new Category();
                category.Name = request.Name;

				await _categoryRepository.AddAsync(category);

				return RedirectToAction(nameof(Index));

			}



			return View(request);

		}


		public async Task<IActionResult> Index()

        {

            var categories = await _categoryRepository.GetAllAsync();

            return View(categories);

        }



        public async Task<IActionResult> Display(int id)

        {

            var category = await _categoryRepository.GetByIdAsync(id);

            if (category == null)

            {

                return NotFound();

            }

            return View(category);

        }



        public async Task<IActionResult> Update(int id)

        {

            var category = await _categoryRepository.GetByIdAsync(id);

            if (category == null)

            {

                return NotFound();

            }

            return View(new UpdateCategoryRequest
            {
                Id = category.Id,
                Name= category.Name,
            }); ;

        }



        [HttpPost]

        public async Task<IActionResult> Update(UpdateCategoryRequest request)

        {


            if (ModelState.IsValid)

            {
                var category = await _categoryRepository.GetByIdAsync(request.Id);

                if (category == null)
                {
                    return NotFound();
                }

                category.Name = request.Name;

                await _categoryRepository.UpdateAsync(category);

                return RedirectToAction(nameof(Index));

            }

            return View(request);

        }


        public async Task<IActionResult> Delete(int id)

        {

            var category = await _categoryRepository.GetByIdAsync(id);

            if (category == null)

            {

                return NotFound();

            }

            return View(category);

        }



        [HttpPost, ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _categoryRepository.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
