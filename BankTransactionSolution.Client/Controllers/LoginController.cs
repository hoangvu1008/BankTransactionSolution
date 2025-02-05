using BankTransactionSolution.Authentication;
using BankTransactionSolution.Domain.Model;
using BankTransactionSolution.Services.Imp;
using BankTransactionSolution.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

            return Ok(new { Message = "Đăng nhập thành công!", Token = accessToken });
        }



    }
}
