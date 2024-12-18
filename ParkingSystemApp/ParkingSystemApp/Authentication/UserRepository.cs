using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ParkingSystemApp.AuthenticationService.Models
{
    class UserRepository : IUserRepository
    {
        private List<User> _users;
        private readonly ILogger<UserRepository> _logger;
        private string configFilePath = "Authentication/UserConfig.json";


        public UserRepository(ILogger<UserRepository> logger)
        {
            _logger = logger;
            LoadUsers();
            _logger.LogInformation("UserRepository initialized.");
        }

        private void LoadUsers()
        {
            if (File.Exists(configFilePath))
            {
                try
                {
                    var json = File.ReadAllText(configFilePath);
                    _users = JsonConvert.DeserializeObject<List<User>>(json) ?? new List<User>();
                    _logger.LogInformation("Loaded {Count} users from {Path}.", _users.Count, configFilePath);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to load users from {Path}.", configFilePath);
                    _users = new List<User>();
                }
            }
            else
            {
                _logger.LogWarning("Users file not found: {Path}. Starting with an empty user list.", configFilePath);
                _users = new List<User>();
            }
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _users;
        }

        public User? GetUser(string username)
        {
            var user = _users.Find(u => u.Username == username);
            if (user != null)
            {
                _logger.LogInformation("User {Username} found.", username);
            }
            else
            {
                _logger.LogWarning("User {Username} not found.", username);
            }
            return user;
        }
    }
}
