using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSystemApp.AuthenticationService.Models
{
    interface IUserRepository
    {
        IEnumerable<User> GetAllUsers();
        User GetUser(string username);
    }
}
