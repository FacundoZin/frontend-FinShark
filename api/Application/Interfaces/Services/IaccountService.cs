using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Application.DTOs.Account;
using api.Domain.Entities;

namespace api.Application.Interfaces.Services
{
    public interface IaccountService
    {
        Task<UserDto> LoginAsync(LoginDto request);
        Task<UserDto> RegisterAsync(RegisterDto request);
        Task<AppUser?> FindByname(string name);
    }
}