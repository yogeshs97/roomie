using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Login.Models;
using Login.Models.ViewModel;
using Login.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Login.Controllers
{

    [ApiController]
    public class AccountController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(AccountController));
        private IConfiguration _config;
        private IAccount accountRepo;
        //private readonly AppDbContext dbContext;
        public AccountController(IConfiguration config, IAccount account)
        {
            //this.dbContext = context;
            this._config = config;
            this.accountRepo = account;
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("/api/Account/Login")]
        public async Task<IActionResult> Login([FromBody]AccountLoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }


            var RightUser = await accountRepo.GetAccount(model); //dbContext.AccLogin.Where(x => x.Username == model.Username && x.Password==model.Password).FirstOrDefaultAsync();
            if (RightUser == null)
            {
                ModelState.AddModelError("", "Invalid Username or Password");
                return NotFound();
            }
            else
            {
                _log4net.Info("Login credential matched");
                return Ok(new
                {
                    token = GenerateJWT(RightUser)
                });
            }
        }

        [HttpGet]
        [Route("/api/Account/GetMessage")]
        public IActionResult GetMessage()
        {
            return Content("HEy there");
        }
        private string GenerateJWT(AccountLogin userLogin)
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}