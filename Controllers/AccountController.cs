using Microsoft.AspNetCore.Mvc;
using AuthApplication.Models;
using AuthApplication.Services;
using System;

namespace AuthApplication.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly EmailService _emailService;

        public AccountController(IUserService userService, EmailService emailService)
        {
            _userService = userService;
            _emailService = emailService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = _userService.GetUserByEmail(email);
            if (user != null && _userService.VerifyPassword(email, password))
            {
                if (!user.IsActive)
                {
                    ModelState.AddModelError("", "The account is not active.");
                    return View();
                }

                _userService.UpdateLastLogin(user.Id);
                HttpContext.Session.SetString("UserId", user.Id.ToString());
                return RedirectToAction("Profile", "Account");
            }

            ModelState.AddModelError("", "Invalid credentials.");
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = _userService.GetUserByEmail(user.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError("", "User already exists.");
                    return View(user);
                }

                var newUser = _userService.RegisterUser(user);

                var activationLink = Url.Action(
                    "Activate",
                    "Account",
                    new { userId = newUser.Id },
                    protocol: HttpContext.Request.Scheme);

                _emailService.SendEmail(
                    newUser.Email,
                    "Account Activation",
                    $"<p>Thank you for registering. Activate your account by clicking the link below:</p><a href='{activationLink}'>Activate Account</a>");

                ViewBag.Message = "Registration successful. Check your email to activate your account.";
                return View();
            }

            return View(user);
        }

        [HttpGet]
        public IActionResult Activate(int userId)
        {
            var result = _userService.ActivateUser(userId);
            ViewBag.Message = result
                ? "Account successfully activated. You can now log in."
                : "Failed to activate account.";
            return View();
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword(string email)
        {
            var user = _userService.GetUserByEmail(email);
            if (user == null)
            {
                ModelState.AddModelError("", "No user found with this email.");
                return View();
            }

            var resetLink = Url.Action(
                "ResetPassword",
                "Account",
                new { email = user.Email },
                protocol: HttpContext.Request.Scheme);

            _emailService.SendEmail(
                user.Email,
                "Password Recovery",
                $"<p>Click the link below to reset your password:</p><a href='{resetLink}'>Reset Password</a>");

            ViewBag.Message = "Password recovery email sent.";
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string email)
        {
            ViewBag.Email = email;
            return View();
        }

        [HttpPost]
        public IActionResult ResetPassword(string email, string newPassword)
        {
            var user = _userService.GetUserByEmail(email);
            if (user == null)
            {
                ModelState.AddModelError("", "User not found.");
                return View();
            }

            user.Password = newPassword;
            _userService.UpdateUserInformation(user);

            ViewBag.Message = "Password successfully reset. You can now log in.";
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UserId");
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Profile()
        {
            var userIdStr = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdStr))
                return RedirectToAction("Login");

            int userId = int.Parse(userIdStr);
            var currentUser = _userService.GetUserById(userId);
            if (currentUser == null) return RedirectToAction("Login");

            var profileViewModel = new UserProfileViewModel
            {
                Id = currentUser.Id,
                Email = currentUser.Email,
                FirstName = currentUser.FirstName,
                LastName = currentUser.LastName,
                Address = currentUser.Address,
                BirthDate = currentUser.BirthDate
            };

            return View(profileViewModel);
        }

        [HttpPost]
        public IActionResult Profile(UserProfileViewModel updatedProfile)
        {
            if (ModelState.IsValid)
            {
                var currentUser = _userService.GetUserById(updatedProfile.Id);
                if (currentUser == null)
                {
                    ViewBag.Message = "User not found.";
                    return View(updatedProfile);
                }

                currentUser.FirstName = updatedProfile.FirstName;
                currentUser.LastName = updatedProfile.LastName;
                currentUser.Address = updatedProfile.Address;
                currentUser.BirthDate = updatedProfile.BirthDate;

                var result = _userService.UpdateUserInformation(currentUser);
                ViewBag.Message = result != null
                    ? "Information updated successfully."
                    : "Failed to update information.";
                return View(updatedProfile);
            }

            return View(updatedProfile);
        }
    }
}
