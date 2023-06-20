using AutoMapper;
using CRUDTest.Application.Common.Exceptions;
using CRUDTest.Application.Common.Interfaces;
using CRUDTest.Domain.Entities;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CRUDTest.Infrastructure
{
    public sealed class UserAuthenticationRepository : IUserAuthenticationService
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private User? _user;

        public UserAuthenticationRepository(UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<Result> RegisterUserAsync(string userName, string password)
        {
            var user = new User { UserName = userName, PasswordHash = password };
            var result = await _userManager.CreateAsync(user, password);
            return result.ToApplicationResult();
        }

        public async Task<bool> ValidateUserAsync(string userName, string password)
        {
            var user = new User { UserName = userName, PasswordHash = password };
            _user = await _userManager.FindByNameAsync(user.UserName);
            var result = _user != null && await _userManager.CheckPasswordAsync(_user, password);
            return result;
        }

        public async Task<string> CreateTokenAsync()
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        private SigningCredentials GetSigningCredentials()
        {
            var jwtConfig = _configuration.GetSection("jwtConfig");
            var key = Encoding.UTF8.GetBytes(jwtConfig["Secret"]);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, _user.UserName)
        };
            var roles = await _userManager.GetRolesAsync(_user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JwtConfig");
            var tokenOptions = new JwtSecurityToken
            (
            issuer: jwtSettings["validIssuer"],
            audience: jwtSettings["validAudience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expiresIn"])),
            signingCredentials: signingCredentials
            );
            return tokenOptions;
        }


    }
}
