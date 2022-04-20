using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkManagement.Services
{
    public interface IAuthenticate
    {
        Task<bool> Authenticate(string loginCNPJ, string password);
        Task<bool> RegisterUser(string loginCNPJ, string password);
        Task Logout();
    }
}
