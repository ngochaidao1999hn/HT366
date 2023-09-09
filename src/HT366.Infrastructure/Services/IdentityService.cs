using HT366.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HT366.Infrastructure.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
        private readonly IConfiguration _configuration;

        public IdentityService(UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
            _configuration = configuration;
        }

        public async Task<List<ApplicationUser>> GetListUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            return users;
        }

        public async Task<string?> GetUserNameAsync(Guid userId)
        {
            var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

            return user.UserName;
        }

        public async Task<IdentityResult> CreateUserAsync(string email, string password)
        {
            var user = new ApplicationUser
            {
                UserName = email,
                Email = email,
            };

            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                var defaultRole = await _roleManager.FindByNameAsync("client");
                if (defaultRole is not null)
                {
                    await _userManager.AddToRoleAsync(user, defaultRole.Name);
                }
            }
            return result;
        }

        public async Task<bool> IsInRoleAsync(Guid userId, string role)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            return user != null && await _userManager.IsInRoleAsync(user, role);
        }

        public async Task<Tuple<string?, DateTime?>> AuthorizeAsync(string userName, string password)
        {
            ApplicationUser? user = _userManager.Users.SingleOrDefault(u => u.UserName == userName);
            Tuple<string?, DateTime?> res = new Tuple<string?, DateTime?>(null, null);
            if (user is not null)
            {
                if (await _userManager.CheckPasswordAsync(user, password))
                {
                    var userClaimPrincipals = await _userClaimsPrincipalFactory.CreateAsync(user);
                    var claims = new List<Claim>();
                    claims.Add(new Claim("UserId", user.Id.ToString()));
                    claims.Add(new Claim("Email", user.Email ?? ""));
                    claims.Add(new Claim("UserName", user.UserName ?? ""));
                    foreach (var role in await _userManager.GetRolesAsync(user))
                    {
                        claims.Add(new Claim("Role", role));
                    }
                    var issuer = _configuration["JWT:Issuer"];
                    var audience = _configuration["JWT:Audience"];
                    var securityKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
                    var credentials = new SigningCredentials(securityKey,
                    SecurityAlgorithms.HmacSha256);
                    var expDate = DateTime.Now.AddDays(1);
                    var token = new JwtSecurityToken(issuer: issuer,
                        audience: audience,
                        signingCredentials: credentials,
                        claims: claims,
                        expires: expDate);
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var stringToken = tokenHandler.WriteToken(token);
                    res = new Tuple<string?, DateTime?>(stringToken, expDate);
                }
            }
            return res;
        }

        public async Task<bool> DeleteUserAsync(Guid userId)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);
            return user != null ? await DeleteUserAsync(user) : false;
        }

        public async Task<bool> DeleteUserAsync(ApplicationUser user)
        {
            var result = await _userManager.DeleteAsync(user);

            return result.Succeeded;
        }

        public async Task<ApplicationUser?> GetByIdAsync(Guid userId)
        {
            return await _userManager.Users.SingleOrDefaultAsync(u => u.Id == userId);
        }
    }
}