using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ProductsSite;
/*[Authorize(Roles="admin")]*/
public class UserList : PageModel
{
    public UserList(UserManager<SiteUsers> userManager)
    {
        _userManager = userManager;
    }

    private UserManager<SiteUsers> _userManager { get; }
    
    public List<SiteUsers>? AllUsers { get; private set; }
    
    public IActionResult OnGet()
    {
        if (!User.IsInRole("Admin"))
            return Forbid();
        AllUsers = _userManager.Users.ToList();
        return Page();
    }
    
}