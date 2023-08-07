using System.Security.Principal;
using ASP.NET_HW_9.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASP.NET_HW_9.Pages;

public class IndexModel : PageModel {
    private DataContext _dataContext;

    private readonly ILogger<IndexModel> _logger;

    public IndexModel(DataContext dataContext, ILogger<IndexModel> logger) {
        _dataContext = dataContext;
        _logger = logger;
    }

    public IIdentity? Identity { get; set; }

    public void OnGet() {
        Identity = User.Identity;
        _logger.LogInformation(User.Identity.Name);
    }

}