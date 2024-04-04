using Bai2.Models;
using Microsoft.AspNetCore.Identity;

namespace Bai2.Repository
{
	public interface IOrderRepository
	{
		Task<IEnumerable<Order>> GetAllAsync(IdentityUser user);

		Task<Order> GetByIdAsync(int id);
	}
}
