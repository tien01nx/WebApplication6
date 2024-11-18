using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using WebApplication6.Models.db;

namespace WebApplication6.Views.Home
{
    public class AccountController : Controller
    {
        private readonly Demo2Context _context;

        public AccountController(Demo2Context context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = _context.Users.SingleOrDefault(u => u.Username == username && u.Password == password);
            if (user == null)
            {
                ModelState.AddModelError("", "Thông tin đăng nhập không đúng.");
                return View();
            }

            var userGroup = _context.SpGroupUsers.SingleOrDefault(gu => gu.Username == username)?.GroupName;
            if (userGroup == null) return Unauthorized();

            var permissions = _context.SpGroupPers
                .Where(gp => gp.GroupName == userGroup)
                .Select(gp => new { gp.Form, gp.Action })
                .ToList();
            // Chuyển đổi danh sách permissions thành JSON
            var permissionsJson = JsonConvert.SerializeObject(permissions);

            // Lưu JSON vào Session
            HttpContext.Session.SetString("UserPermissions", permissionsJson);

            // Xác thực người dùng
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, username) };
            var identity = new ClaimsIdentity(claims, "Cookies");
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync("Cookies", principal);

            return RedirectToAction("Index", "Home");
        }
    }
}
