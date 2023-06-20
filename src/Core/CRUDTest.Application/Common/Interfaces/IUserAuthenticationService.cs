using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDTest.Application.Common.Interfaces;

public interface IUserAuthenticationService
{
    Task<Result> RegisterUserAsync(string userName, string password);
    Task<bool> ValidateUserAsync(string username, string password);
    Task<string> CreateTokenAsync();
}
