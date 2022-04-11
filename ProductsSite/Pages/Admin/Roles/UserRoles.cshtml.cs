using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ProductsSite;
[Authorize(Roles="admin")]
public class UserRoles : PageModel
{
    RoleManager<IdentityRole> _roleManager { get; set; }
    UserManager<SiteUsers> _userManager { get; set; }

    public IList<string> CurrentRoles { get; set; }

    public List<IdentityRole> AllRoles { get; set; }

    public SiteUsers CurrentUser { get; set; }

    public UserRoles(RoleManager<IdentityRole> roleManager, UserManager<SiteUsers> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    /*public IActionResult UserList() => View(_userManager.Users.ToList());*/

    public async Task<IActionResult> OnGetAsync(string userId)
    {
        // получаем пользователя
        CurrentUser = await _userManager.FindByIdAsync(userId);
        if (CurrentUser is null)
            return NotFound();
        // получем список ролей пользователя
        CurrentRoles = await _userManager.GetRolesAsync(CurrentUser);
        AllRoles = _roleManager.Roles.ToList();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(string userId, List<string> roles)
    {
        // получаем пользователя
        CurrentUser = await _userManager.FindByIdAsync(userId);
        if (CurrentUser is null)
            return NotFound();
        // получем список ролей пользователя
        CurrentRoles = await _userManager.GetRolesAsync(CurrentUser);
        // получаем все роли
        AllRoles = _roleManager.Roles.ToList();
        // получаем список ролей, которые были добавлены
        var addedRoles = roles.Except(CurrentRoles);
        // получаем роли, которые были удалены
        var removedRoles = CurrentRoles.Except(roles);

        await _userManager.AddToRolesAsync(CurrentUser, addedRoles);

        await _userManager.RemoveFromRolesAsync(CurrentUser, removedRoles);

        return Redirect("/Admin/UserList");
    }
}
