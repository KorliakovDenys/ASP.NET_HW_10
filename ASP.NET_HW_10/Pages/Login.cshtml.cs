using System.Security.Claims;
using ASP.NET_HW_9.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_HW_9.Pages {
    public class Login : PageModel {
        private DataContext _dataContext;

        private ILogger<IndexModel> _logger;

        private IHttpContextAccessor _httpContextAccessor;

        public Login(DataContext context, ILogger<IndexModel> logger, IHttpContextAccessor httpContextAccessor) {
            _dataContext = context;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync(string? returnUrl) {
            var form = _httpContextAccessor.HttpContext?.Request.Form;

            if (form == null || !form.ContainsKey("email") || !form.ContainsKey("password"))
                return BadRequest("Email or password is not set.");

            string email = form["email"]!;
            string password = form["password"]!;

            var user = await _dataContext.Users!.FirstOrDefaultAsync(u => u.Login == email && u.Password == password);
            if (user is null) return Unauthorized();

            var claims = new List<Claim> {
                new("Id", user.Id.ToString()),
                new(ClaimTypes.Name, user.Login),
                new(ClaimTypes.Role, user.Role.ToString()),
            };
            var claimsIdentity = new ClaimsIdentity(claims, "Cookies");

            await _httpContextAccessor.HttpContext!.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));

            return Redirect(returnUrl ?? "/Index");
        }
    }
}