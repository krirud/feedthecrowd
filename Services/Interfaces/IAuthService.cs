using FeedTheCrowd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FeedTheCrowd.Dtos.Auth;

namespace FeedTheCrowd.Services.Interfaces
{
    public interface IAuthService
    {
        Task Register(UserForRegistrationDto userForRegistrationDto);
        Task<User> Login(string username, string password);
    }
}
