using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASP.NET_HW_9.Pages {
    public class Logout : PageModel {
        private ILogger<IndexModel> _logger;

        private IHttpContextAccessor _httpContextAccessor;

        public Logout(ILogger<IndexModel> logger, IHttpContextAccessor httpContextAccessor) {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> OnGetAsync() {
            await _httpContextAccessor.HttpContext?.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme)!;
            return Redirect("/Index");
        }
    }
}