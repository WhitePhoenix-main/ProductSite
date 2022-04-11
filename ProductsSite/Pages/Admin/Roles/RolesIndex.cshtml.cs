using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ProductsSite;
[Authorize(Roles="admin")]
public class RolesIndex : PageModel
{
    public RolesIndex(RoleManager<IdentityRole> roleManager, string userId, string userEmail, List<IdentityRole> allRoles, IList<string> userRoles)
    {
        _roleManager = roleManager;
        UserId = userId;
        UserEmail = userEmail;
        AllRoles = allRoles;
        UserRoles = userRoles;
    }

    private RoleManager<IdentityRole> _roleManager { get; }
        
    public string UserId { get; set; }
    public string UserEmail { get; set; }
    public List<IdentityRole> AllRoles { get; set; }
    public IList<string> UserRoles { get; set; }
    public void OnGet()
    {
        AllRoles = _roleManager.Roles.ToList();
        UserRoles = new List<string>();
    }
}
