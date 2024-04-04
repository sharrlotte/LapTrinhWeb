using Bai2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Bai2.Repository
{
	public class EFOrderRepository : IOrderRepository
	{
		private readonly ApplicationDbContext _context;

		public EFOrderRepository(ApplicationDbContext context)
		{
			_context = context;
		}


		public async Task<IEnumerable<Order>> GetAllAsync(IdentityUser user)
		{
			return await _context.Orders.Where(p => p.UserId == user.Id).ToListAsync();

		}

		public async Task<Order> GetByIdAsync(int id)
		{
			return await _context.Orders.FindAsync(id);
		}

	}
}
