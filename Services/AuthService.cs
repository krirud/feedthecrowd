using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FeedTheCrowd.Data.Interfaces;
using FeedTheCrowd.Dtos.Auth;
using FeedTheCrowd.Models;
using FeedTheCrowd.Services.Interfaces;

namespace FeedTheCrowd.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _repository;
        public AuthService(IAuthRepository repository)
        {
            _repository = repository;
        }

        public async Task Register(UserForRegistrationDto userForRegistrationDto)
        {
            var userToCreate = new User
            {
                Name = userForRegistrationDto.Name,
                Surname = userForRegistrationDto.Surname,
                Username = userForRegistrationDto.Username,
                Email = userForRegistrationDto.email,
            };

            await _repository.Register(userToCreate, userForRegistrationDto.Password);
        }

        public async Task<User> Login(string username, string password)
        {
            return await _repository.Login(username, password);
        }
    }
}
