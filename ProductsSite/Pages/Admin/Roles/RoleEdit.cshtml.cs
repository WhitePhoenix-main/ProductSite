using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ProductsSite;
[Authorize(Roles="admin")]
public class RoleEdit : PageModel
{
    private RoleManager<IdentityRole> _roleManager { get;}


    public List<IdentityRole>? RolesList { get; set; }

    public string? RoleName { get; set; }

    public bool IsNewRec { get; private set; }

    public RoleEdit(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public IActionResult OnGetCreate()
    {
        if (!User.IsInRole("admin"))
            return Forbid();
        IsNewRec = true;
        return Page();
    }

    public IActionResult OnGetEdit(string roleName)
    {
        if (!User.IsInRole("admin"))
            return Forbid();
        IsNewRec = false;
        RoleName = roleName;
        return Page();
    }

    public async Task<IActionResult> OnGetDeleteAsync(string id)
    {
        if (!User.IsInRole("admin"))
            return Forbid();
        IdentityRole role = await _roleManager.FindByIdAsync(id);
        if (role != null)
        {
            IdentityResult result = await _roleManager.DeleteAsync(role);
        }

        return RedirectToPage("Roles/RolesIndex");
    }

    public async Task<IActionResult> OnPostCreateAsync(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            ModelState.AddModelError("name", "Название не может быть пустым");
            return Page();
        }

        var result = await _roleManager.CreateAsync(new IdentityRole(name));
        if (result.Succeeded)
            return RedirectToAction("Roles/RolesIndex");
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError("name", error.Description);
        }

        return Page();
    }
    
    public async Task<IActionResult> Delete(string id)
    {
        IdentityRole role = await _roleManager.FindByIdAsync(id);
        if (role != null)
        {
            IdentityResult result = await _roleManager.DeleteAsync(role);
        }

        return RedirectToAction("Index");
    }

}