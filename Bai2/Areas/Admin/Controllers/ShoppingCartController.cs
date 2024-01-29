using Bai2.Models;
using Microsoft.AspNetCore.Mvc;
using Bai2.Utils;
using Bai2.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Bai2.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class ShoppingCartController : Controller

    {


 
        private readonly IProductRepository _productRepository;

        private readonly ApplicationDbContext _context;

        private readonly UserManager<IdentityUser> _userManager;

        public ShoppingCartController(IProductRepository productRepository, ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _productRepository = productRepository;
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Checkout()

        {

            return View(new Order());

        }



        [HttpPost]

        public async Task<IActionResult> Checkout(Order order)

        {

            var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart");

            if (cart == null || !cart.Items.Any())

            {

                // Xử lý giỏ hàng trống... 

                return RedirectToAction("Index");

            }



            var user = await _userManager.GetUserAsync(User);

            order.UserId = user.Id;

            order.OrderDate = DateTime.UtcNow;

            order.TotalPrice = cart.Items.Sum(i => i.Price * i.Quantity);

            order.OrderDetails = cart.Items.Select(i => new OrderDetail

            {

                ProductId = i.ProductId,

                Quantity = i.Quantity,

                Price = i.Price

            }).ToList();



            _context.Orders.Add(order);

            await _context.SaveChangesAsync();



            HttpContext.Session.Remove("Cart");



            return View("OrderCompleted", order.Id); // Trang xác nhận hoàn thành đơn hàng 

        }

     

        public async Task<IActionResult> AddToCart(int productId, int quantity)

        {

            // Giả sử bạn có phương thức lấy thông tin sản phẩm từ productId 

            var product = await GetProductFromDatabase(productId);



            var cartItem = new CartItem

            {

                ProductId = productId,

                Name = product.Name,

                Price = product.Price,

                Quantity = quantity

            };



            var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart") ?? new ShoppingCart();

            cart.AddItem(cartItem);



            HttpContext.Session.SetObjectAsJson("Cart", cart);



            return RedirectToAction("Index");

        }



        public IActionResult Index()

        {

            var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart") ?? new ShoppingCart();

            return View(cart);

        }



        // Các actions khác... 



        private async Task<Product> GetProductFromDatabase(int productId)

        {
            return await _productRepository.GetByIdAsync(productId);
        }

    }
}
