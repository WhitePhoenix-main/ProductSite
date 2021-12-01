using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace ProductsSite.Pages.Admin.Roles
{
//    [Authorize(Roles="admin")]
    public class ListRoles : PageModel
    {
        public ListRoles(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
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
}