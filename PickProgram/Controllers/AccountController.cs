using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PickProgram.ViewModels;

namespace PickProgram.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<IActionResult> AddUser(string id, string id2)
        {
            var newUser = new IdentityUser() { UserName = id };
            IdentityResult result = await _userManager.CreateAsync(newUser, id2);

            return RedirectToAction("Main", "Dashboard");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel lvm)
        {
            if (ModelState.IsValid)
            {
                var foundUser = await _userManager.FindByNameAsync(lvm.Username);
                if (foundUser != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(foundUser, lvm.Password, false, false);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Main", "Dashboard");
                    }
                    else
                    {
                        ModelState.AddModelError("", "The password entered does not match our records. Please try again.");
                        return View(lvm);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Username was not found. Please try again.");
                    return View(lvm);
                }
            }
            //necesarry to pass viewmodel back in?
            return View(lvm);
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

    }
}