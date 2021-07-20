using MacPartners.Domain.Interfaces;
using MacPartners.Domain.Models;
using MacPartners.Domain.Models.ValueObjects;
using MacPartners.Domain.Queries;
using MacPartners.Domain.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace MacPartners.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IUserRepository _repository;
        private readonly ICrypter _crypter;
        private readonly IEmail _emailService;
        private readonly Partner _partner;
        private readonly IHttpContextAccessor _httpContextAcessor;
        private readonly PartnersQueries _partnerQueries;

        public AccountController(ICrypter crypter, IEmail emailService, IHttpContextAccessor httpContextAccessor, IUserRepository userRepository, IPartnerRepository partnerRepository)
        {
            _repository = userRepository;
            _crypter = crypter;
            _emailService = emailService;
            _httpContextAcessor = httpContextAccessor;
            _partnerQueries = new PartnersQueries(partnerRepository);

            var isLogged = httpContextAccessor.HttpContext.User.Identities.First() != null;

            if (isLogged)
            {
                var userName = httpContextAccessor.HttpContext.User.Identities.First().Name;
                _partner = _partnerQueries.PartnerByUserEmail(userName);
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = _repository.Find(email, _crypter.Encrypt(password));

            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.Person.Email.EmailAdress),
                new Claim("FullName", user.Person.Email.EmailAdress),
                new Claim(ClaimTypes.Role, user.UserRole.ToString()),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                RedirectUri = "/Home/Index",
            };

            HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();

            return RedirectToAction("Login", "Account");
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult ForgotMyPassword()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult ForgotMyPassword(string email)
        {
            ViewBag.Email = email;

            var user = _repository.Find(email);

            if (user == null)
            {
                ViewBag.Faild = true;
                ViewBag.Notifications = "E-mail não encontrado em nossa base de clientes";
            }
            else
            {
                var domain = Request.Host.ToUriComponent();
                var newPasswordUrl = domain + "/macpartners/Account/CreateNewPassword/" + user.Id;
                var response = _emailService.Send(user.Person.Email, "MacPartners - Nova Senha", "Use o endereço a seguir para mudar sua senha:<br/><a href='" + newPasswordUrl + "' target='_blank'>" + newPasswordUrl + "</a>");

                if (!response.Message.Contains("sucesso"))
                {
                    ViewBag.Faild = true;
                    ViewBag.Notifications = "E-mail não encontrado em nossa base de parceiros";
                }
                else
                {
                    ViewBag.Faild = false;
                    ViewBag.Notifications = "Um e-mail foi enviado para " + email + " com o link para alteração de sua senha.";
                }
            }

            return View();
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            var userName = _httpContextAcessor.HttpContext.User.Identities.First().Name;
            var user = _repository.Find(userName);
            ViewBag.UserId = user.Id;

            return View();
        }

        [HttpPost]
        public IActionResult ChangePassword(Guid userId, string curPassword, string newPassword)
        {
            ViewBag.UserId = userId;
            ViewBag.CurPassword = curPassword;
            ViewBag.NewPassword = newPassword;

            var user = _repository.Find(userId);

            if (!_crypter.Encrypt(curPassword).Equals(user.Password))
            {
                ViewBag.Faild = true;
                ViewBag.Notifications = "A senha atual está incorrenta";
            }
            else
            {
                try
                {
                    if (user != null)
                        user.ChangePassword(newPassword, _crypter, _repository);

                    _repository.SaveChanges();
                }
                catch (Exception exception)
                {
                    ViewBag.Faild = true;
                    ViewBag.Notifications = exception.Message;
                    return View();
                }

                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult CreateNewPassword(Guid id)
        {
            ViewBag.UserGuid = id;

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateNewPassword(Guid id, string newPassword)
        {
            ViewBag.UserGuid = id;

            try
            {
                var user = _repository.Find(id);
                if (user != null)
                    user.ChangePassword(newPassword, _crypter, _repository);

                _repository.SaveChanges();
            }
            catch (Exception exception)
            {
                ViewBag.Faild = true;
                ViewBag.Notifications = exception.Message;
                return View();
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
