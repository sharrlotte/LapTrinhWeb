using Bai2.Models;

namespace Bai2.Repository
{
    public interface ICategoryRepository

    {
		Task<IEnumerable<Category>> GetAllAsync();

		Task<Category> GetByIdAsync(int id);

		Task AddAsync(Category category);

		Task UpdateAsync(Category category);

		Task DeleteAsync(int id);

	}
}