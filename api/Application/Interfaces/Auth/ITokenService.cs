using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Domain.Entities;

namespace api.Application.Interfaces.Auth
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}