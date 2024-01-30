using Bai1.Models;

namespace Bai1.Repository
{
    public interface ICategoryRepository

    {
        IEnumerable<Category> GetAllCategories();

    }
}