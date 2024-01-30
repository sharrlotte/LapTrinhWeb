using Bai2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Bai2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager)
        {
           _logger = logger;
            _userManager = userManager;
        }
        // Thêm vào service Role - .AddRoles<IdentityRole>() vào service
        // Thêm Role vào database
        /*public void Configure(IApplicationBuilder app, IServiceProvider serviceProvider)
        {
            CreateRoles(serviceProvider).Wait();
        }

        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roleNames = { "User", "Admin", "Management" };
            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    // Tạo các vai trò mới
                    var roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }*/

        public async Task<IActionResult> Index()
        {
            // Thêm quyền admin cho bản thân
            /* var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                await _userManager.AddToRoleAsync(user,"Admin");
            }*/

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}