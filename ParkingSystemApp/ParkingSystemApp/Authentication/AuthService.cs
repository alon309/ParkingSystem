using ParkingSystemApp.AuthenticationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;


namespace ParkingSystemApp.AuthenticationService
{
    class AuthService : IAuthService
    {
        private readonly ILogger<AuthService> _logger;
        private readonly IUserRepository _userRepository;

        public AuthService(ILogger<AuthService> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
            _logger.LogInformation("AuthenticationService Initialized");
        }

        public bool Authenticate(string username, string password)
        {
            var user = _userRepository.GetUser(username);

            if (user == null)
            {
                _logger.LogInformation("Login Failed");
                return false;
            }

            var result = user.Password == password;
            _logger.LogInformation(result ? "Login Successfull. {username} is logged in" : "Login Failed. Incorrect password for {username}", username);
            return result;
        }
    }
}
