using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Task5.DAL.Entities;
using Task5.WebApi.Interfaces;

namespace Task5.WebApi.Security
{
    public class JwtGenerator : IJwtGenerator
    {
        private readonly UserManager<User> userManager;
        public JwtGenerator(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<string> CreateToken(User user)
        {
            if (user != null)
            {
                var identity = await GetIdentity(user);

                var now = DateTime.UtcNow;

                var jwt = new JwtSecurityToken(
                        issuer: AuthOptions.ISSUER,
                        audience: AuthOptions.AUDIENCE,
                        notBefore: now,
                        claims: identity.Claims,
                        expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                        signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                return encodedJwt;
            }
            return "";
        }

        private async Task<ClaimsIdentity> GetIdentity(User user)
        {
            var roles = await userManager.GetRolesAsync(user);
            var role = "";
            if (roles.Any(x => x == "Admin"))
                role = "Admin";
            else if (roles.Any(x => x == "Moderator"))
                role = "Moderator";
            else
                role = "User";

            var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, role)
                };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            return claimsIdentity;
        }
    }
}