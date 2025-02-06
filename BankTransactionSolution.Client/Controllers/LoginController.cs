using BankTransactionSolution.Authentication;
using BankTransactionSolution.Domain.Model;
using BankTransactionSolution.Services.Imp;
using BankTransactionSolution.Services.Interface;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BankTransactionSolution.Client.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserService _userService;
        private readonly ITokenHandler _tokenHandler;

        public LoginController(IUserService userService, ITokenHandler tokenHandler)
        {
            _userService = userService;
            _tokenHandler = tokenHandler;
        }

        public async Task<ActionResult> Login([FromBody] LoginModel loginModel)
        {
            try
            {
                var member = await _userService.GetUserWithConditionn(t => t.user_name == loginModel.user_name && t.password == loginModel.password);
                if (member == null)
                {
                    return NotFound("Tài khoản đăng nhập sai (hoặc đã bị vô hiệu hóa)");
                }

                (string accessToken, DateTime expiredDateAccess) = await _tokenHandler.CreateAccessToken(member);

                Response.Cookies.Append("access_token", accessToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = expiredDateAccess
                });

                HttpContext.Response.Cookies.Append(
                    "user_name",
                    loginModel.user_name,
                    new CookieOptions
                    {
                        Path = "/",
                        HttpOnly = true,
                        Secure = true,
                        Expires = DateTime.UtcNow.AddDays(7)
                    }
                );

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, loginModel.user_name),
                    new Claim("AccessToken", accessToken),
                    new Claim(ClaimTypes.Role, "User")
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                return Json(new { result = true, message = "Login successful." });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


    }
}
