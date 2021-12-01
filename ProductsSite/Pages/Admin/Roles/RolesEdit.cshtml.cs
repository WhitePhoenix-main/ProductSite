using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Site
{
    public class RolesEditModel : PageModel
    {
        public RolesEditModel(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        private RoleManager<IdentityRole> _roleManager { get; }
        [BindProperty(SupportsGet = true)]
        public string? Id { get; set; }
        
        [BindProperty]
        public string? RoleName { get; set; }
        // показать форму для изменения роли
        public async Task<IActionResult> OnGetAsync()
        {
            var role = await _roleManager.FindByIdAsync(Id);
            if (role is null)
                return NotFound();
            if (string.IsNullOrWhiteSpace(Id))
                return BadRequest();
            RoleName = role.Name;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var role = await _roleManager.FindByIdAsync(Id);
            if (role is null)
                return NotFound();
            if (string.IsNullOrWhiteSpace(Id))
                return BadRequest();
            role.Name = RoleName;
            await _roleManager.UpdateAsync(role);
            return Redirect("~/Admin/Roles/ListRoles");
        }
        
        // показать форму для добавления роли
        public IActionResult OnGetAddAsync()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAddAsync()
        {
            if (await _roleManager.RoleExistsAsync(RoleName))
            {
                ModelState.AddModelError(nameof(RoleName), "Такая роль уже есть!");
                return Page();
            }
            var role = new IdentityRole();
            role.Id = Guid.NewGuid().ToString();
            role.Name = RoleName;
            await _roleManager.CreateAsync(role);
            return Redirect("~/Admin/Roles/ListRoles");
        }

        public IActionResult OnPostDelete()
        {
            return Page();
        }

    }
}