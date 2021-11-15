using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task5.DAL.Entities;
using Task5.WebApi.ViewModels.User;

namespace Task5.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UserController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;
        public UserController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        [HttpGet]
        public IEnumerable<IdentityRole> RoleList() => roleManager.Roles.ToList();

        [HttpGet]
        public async Task<IEnumerable<UserViewModel>> UserList()
        {
            var users = userManager.Users.ToList();
            var result = new List<UserViewModel>();
            foreach (var user in users)
            {
                result.Add(new UserViewModel
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Role = await userManager.GetRolesAsync(user)
                });
            }
            return result;
        }

        [HttpPost]
        public async Task<ActionResult<UserViewModel>> AddUser(RegisterViewModel request)
        {
            if (request == null)
                return BadRequest();

            var user = await userManager.FindByEmailAsync(request.Email);
            var roles = roleManager.Roles;

            if (user != null || !roles.Any(x => x.Name == request.Role))
                return BadRequest();

            user = new User { UserName = request.UserName, Email = request.Email };

            await userManager.CreateAsync(user, request.Password);

            await userManager.AddToRoleAsync(user, request.Role);

            var response = new UserViewModel { Email = user.Email, UserName = user.UserName, Role = await userManager.GetRolesAsync(user) };

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<UserViewModel>> AddRole(ChangeRoleViewModel request)
        {
            if (request == null)
                return BadRequest();

            var user = await userManager.FindByEmailAsync(request.Email);
            var roles = roleManager.Roles;

            if (user == null || !roles.Any(x => x.Name == request.Role))
                return NotFound();

            await userManager.AddToRoleAsync(user, request.Role);

            var response = new UserViewModel { Email = user.Email, UserName = user.UserName, Role = await userManager.GetRolesAsync(user) };

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<UserViewModel>> RemoveRole(ChangeRoleViewModel request)
        {
            if (request == null)
                return BadRequest();

            var user = await userManager.FindByEmailAsync(request.Email);
            var roles = roleManager.Roles;

            if (user == null || !roles.Any(x => x.Name == request.Role))
                return NotFound();

            await userManager.RemoveFromRoleAsync(user, request.Role);

            var response = new UserViewModel { Email = user.Email, UserName = user.UserName, Role = await userManager.GetRolesAsync(user) };

            return Ok(response);
        }
    }
}
