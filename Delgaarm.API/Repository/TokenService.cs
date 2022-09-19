using Delgaarm.API.Helper;
using Infrastracture.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.API.Repository
{
    public class TokenService : ITokenService
    {
        private readonly APISettings _apiSettings;
        private readonly UserManager<ApplicationUser> _userManager;
        public TokenService( UserManager<ApplicationUser> userManager, IOptions<APISettings> options)
        {
            _userManager = userManager;
            _apiSettings = options.Value;
        }
        public async Task<string> GetToken(ApplicationUser user)
        {
            var signIncredentials = GetSigningCredentials();
            var claims = await GetClaim(user);

            var tokenOptions = new JwtSecurityToken(
                issuer: _apiSettings.ValidIssuer,
                audience: _apiSettings.ValidAudience,
                claims: claims,
                expires: System.DateTime.Now.AddDays(30),
                signingCredentials: signIncredentials);

            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return token;
            //var claims = new List<Claim>
            //{
            //    new Claim(JwtRegisteredClaimNames.NameId , user.Id.ToString()),
            //    new Claim(JwtRegisteredClaimNames.UniqueName , user.UserName),
            ////};
            //var claims = new List<Claim>()
            //{
            //    new Claim(ClaimTypes.Name, user.UserName),
            //    new Claim(ClaimTypes.Email, user.Email),
            //    new Claim("Id", user.Id)
            //};

            //var roles = await _userManager.GetRolesAsync(user);
            //claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
            //var tokenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_apiSettings.SecretKey));
            //var creds = new SigningCredentials(tokenKey, SecurityAlgorithms.HmacSha512Signature);

            ////var tokenDescriptor = new SecurityTokenDescriptor
            ////{
            ////    Subject = new ClaimsIdentity(claims),
            ////    Expires = DateTime.Now.AddDays(30),
            ////    SigningCredentials = creds
            ////};
            //var tokenOptions = new JwtSecurityToken(
            //       issuer: _apiSettings.ValidIssuer,
            //       audience: _apiSettings.ValidAudience,
            //       claims: claims,
            //       expires: System.DateTime.Now.AddDays(30),
            //       signingCredentials: creds);

            //var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            //return token;
            ////var tokenHandler = new JwtSecurityTokenHandler();
            ////var token = tokenHandler.CreateToken(tokenOptions);

            ////return tokenHandler.WriteToken(token);
        }

        private SigningCredentials GetSigningCredentials()
        {
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_apiSettings.SecretKey));
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaim(ApplicationUser user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("Id", user.Id)
            };

            var roles = await _userManager.GetRolesAsync(await _userManager.FindByEmailAsync(user.Email));
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }
    }
}
