using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Task5.DAL.Entities;
using Task5.WebApi.Interfaces;
using Task5.WebApi.ViewModels.User;

namespace Task5.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [AllowAnonymous]
    public class AccountController : ControllerBase
    {

        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IJwtGenerator jwtGenerator;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IJwtGenerator jwtGenerator)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.jwtGenerator = jwtGenerator;
        }

        [HttpPost]
        public async Task<ActionResult<UserViewModel>> Login(LoginViewModel request)
        {
            if (request == null)
                return BadRequest();

            var user = await userManager.FindByEmailAsync(request.Email);
            if (user == null)
                return Unauthorized();

            var result =  await signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            var token = await jwtGenerator.CreateToken(user);

            Response.Headers.Add("Authorization", "Bearer " + token);

            return new UserViewModel
            {
                UserName = user.UserName,
                Email = user.Email,
                Role = await userManager.GetRolesAsync(user)
            };
        }
    }
}
