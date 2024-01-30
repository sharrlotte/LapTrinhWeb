namespace Bai2.Repository
{
    using Bai2.Models;
    using System.Collections.Generic;

    public interface IProductRepository
    {

		Task<IEnumerable<Product>> GetAllAsync();

		Task<Product> GetByIdAsync(int id);

		Task AddAsync(Product product);

		Task UpdateAsync(Product product);

		Task DeleteAsync(int id);

	}
}