using Bai1.Models;

namespace Bai1.Repository
{


    public class MockCategoryRepository : ICategoryRepository

    {
        private List<Category> _categoryList;

        public MockCategoryRepository()

        {

            _categoryList = new List<Category>

        {

            new Category { Id = 1, Name = "Laptop" },

            new Category { Id = 2, Name = "Desktop" }, 

            // Thêm các category khác 

        };

        }

        public IEnumerable<Category> GetAllCategories()

        {

            return _categoryList;

        }
    }
}