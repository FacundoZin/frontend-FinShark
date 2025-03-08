using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Account;

namespace api.Interfaces
{
    public interface IaccountService
    {
        Task<UserDto> LoginAsync(LoginDto request);
        Task<UserDto> RegisterAsync(RegisterDto request);
    }
}