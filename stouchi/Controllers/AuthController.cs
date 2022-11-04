using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using stouchi.Context;
using stouchi.Dtos;
using stouchi.Models;
using stouchi.Services;


namespace stouchi.Controllers
{
    [EnableCors("AllowOrigin")]
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ApplicationDbContext _context;

        public AccountController(IAccountService accountService, ApplicationDbContext context)
        {
            _accountService = accountService;
            _context = context;

        }

        [Route("login-token")]
        [HttpPost]
        public async Task<IActionResult> GetLoginToken(LoginDto login)
        {
            var result = await _accountService.GetAuthTokens(login);
            if (result == null)
            {
                return ValidationProblem("invalid credentials");
            }
            return Ok(result);
        }
        [HttpGet]
        [Route("test-auth")]
        [Authorize]
        public IActionResult GetTest()
        {
            return Ok("Only authenticated user can consume this endpoint");
        }

        [HttpPost]
        [Route("renew-tokens")]
        public async Task<IActionResult> RenewTokens(RefreshTokenDto refreshToken)
        {
            var tokens = await _accountService.RenewTokens(refreshToken);
            if (tokens == null)
            {
                return ValidationProblem("Invalid Refresh Token");
            }
            return Ok(tokens);
        }
        [EnableCors("AllowOrigin")]
        [HttpPost]
        [Route("register")]

        public async Task<IActionResult> CreateUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok();
        }


    }
}

