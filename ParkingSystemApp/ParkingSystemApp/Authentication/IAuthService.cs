using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSystemApp.AuthenticationService.Models
{
    interface IAuthService
    {
        public bool Authenticate(string username, string password);
    }
}
