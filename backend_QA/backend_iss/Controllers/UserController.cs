using backend_iss.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using backend_iss.DTOs;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace backend_iss.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly DataContext _dataContext;
        private readonly IConfiguration _configuration;

        public UserController(DataContext dataContext,IConfiguration configuration)
        {
            _dataContext = dataContext;
            _configuration = configuration;
        }
        [HttpPost]
        [Route("CorsTest")]
        public async Task<ActionResult> CorsTest(CorsTest corsTest)
        {
            Console.WriteLine("test");
            Console.WriteLine("val1" + corsTest.val1);
            Console.WriteLine("val2"+corsTest.val2);
            return Ok();
        }

        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult<List<User>>> Register(UserRegisterDto request)
        {
            List<User> list=_dataContext.Users.Where(u => u.Username == request.Username).ToList();

            if(list.Count == 0)
            {

                CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

                User user = new User();
                user.Username = request.Username;
                user.Role = request.Role;
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;


               _dataContext.Users.Add(user); 
                await _dataContext.SaveChangesAsync();

                return Ok();
            }

            return BadRequest("Username Already Exists");
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<string>> Login(UserLoginDto request)
        {

            List<User> list = _dataContext.Users.Where(u => u.Username == request.Username).ToList();
            if(list == null || list.Count == 0)
            {
                return BadRequest("Wrong Credentials");
            }

            if (!VerifyPasswordHash(request.Password, list.ElementAt(0).PasswordHash, list.ElementAt(0).PasswordSalt))
            {
                return BadRequest("Wrong Credentials");
            }


          
            string token = CreateToken(list.ElementAt(0));

            return Ok(token);
       
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }


    }
}
