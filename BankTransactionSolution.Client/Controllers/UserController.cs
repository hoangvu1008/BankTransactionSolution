using BankTransactionSolution.Services.Imp;
using BankTransactionSolution.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BankTransactionSolution.Client.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetUser()
        {
            try
            {
                var userName = Request.Cookies["user_name"];

                if (string.IsNullOrEmpty(userName))
                {
                    return BadRequest(new { message = "Không tìm thấy user_name trong cookies." });
                }

                var user = await _userService.GetUserWithConditionn(t => t.user_name == userName);

                if (user == null)
                {
                    return NotFound(new { message = "Không tìm thấy user." });
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

        }

        public async Task<IActionResult> UserDetail()
        {
            return View();
        }
    }
}
