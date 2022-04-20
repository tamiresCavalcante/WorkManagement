using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkManagement.Services
{
    public class AuthenticateService : IAuthenticate
    {
        private readonly SignInManager<IdentityUser> _signIn;
        private readonly UserManager<IdentityUser> _user;

        public AuthenticateService(SignInManager<IdentityUser> signin, UserManager<IdentityUser> user)
        {
            _signIn = signin;
            _user = user;
        }
        public async Task<bool> Authenticate(string loginCNPJ, string password)
        {
            var result = await _signIn.PasswordSignInAsync(loginCNPJ, password, false, lockoutOnFailure: false);
            return result.Succeeded;
        }

        public async Task Logout()
        {
            await _signIn.SignOutAsync();
        }

        public async Task<bool> RegisterUser(string loginCNPJ, string password)
        {
            var ManagerUser = new IdentityUser
            {
                UserName = loginCNPJ
            };

            var result = await _user.CreateAsync(ManagerUser, password);
            if (result.Succeeded)
            {
                await _signIn.SignInAsync(ManagerUser, isPersistent: false);
            }
            return result.Succeeded;
        }
    }
}
