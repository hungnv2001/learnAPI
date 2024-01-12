using eShopSolution.Aplication.System.Users;
using eShopSolution.ViewModel.System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eShopSolution.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromForm] LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var resultTonken = await _userService.Authencate(request);
            if (string.IsNullOrEmpty(resultTonken))
            {
                return BadRequest("UserName or Pass Wrong");
            }
            return Ok(new { token = resultTonken });
        }
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromForm]RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _userService.Resister(request);
            if (!result)
            {
                return BadRequest("Register is unsucccesful");
            }
            return Ok();
        }

    }
}
