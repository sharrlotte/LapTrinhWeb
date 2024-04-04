using Bai2.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bai2.Controllers
{
	public class OrderController : Controller
	{
		private readonly UserManager<IdentityUser> _userManager;
		private readonly IOrderRepository _orderRepository;
		public OrderController(IOrderRepository orderRepository, UserManager<IdentityUser> userManager)
		{
			_orderRepository = orderRepository;
			_userManager = userManager;
		}

		public async Task<IActionResult> Index()

		{
			var user = await _userManager.GetUserAsync(User);
			var orders = await _orderRepository.GetAllAsync(user);

			return View(orders);

		}

	}
}
