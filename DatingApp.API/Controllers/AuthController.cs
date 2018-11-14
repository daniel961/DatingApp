using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;

        public AuthController(IAuthRepository repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        [HttpPost("register")]
        // we using UserForRegisterDto because the methode get data from json so we dont need to parse it like that
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
           
            // we making the string to lower to handle duplicates in our db like 'jhon' and 'Jhon'
            userForRegisterDto.Username = userForRegisterDto.Username.ToLower();

            //cheking if the username already exist in our db and returning Action result
            if(await _repo.UserExists(userForRegisterDto.Username))
            {
                return BadRequest("Username already exist");
            }

            //if the user not exist we can create hin in out db like below
            // we creating user obj and fill it with the username we have then we cakk the register method in our repository
            
            var userToCreate = new User
            {
                Username = userForRegisterDto.Username
            };

            var createdUser = await _repo.Register(userToCreate, userForRegisterDto.Password);
            
            return StatusCode(201);

        }
            [HttpPost("login")]
            public async Task<IActionResult> Login(UserForLoginDto userForLoginDto){

                
                var userFromRepo = await _repo.Login(userForLoginDto.Username.ToLower(), userForLoginDto.Password);

                if(userFromRepo == null){
                    //could happend from username or password
                    return Unauthorized();
                }

                //token
                var claims = new[]{
                    new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                    new Claim(ClaimTypes.Name, userFromRepo.Username)
                };
                var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_config.GetSection("AppSettings:Token").Value));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                var tokenDescripter = new SecurityTokenDescriptor{
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = creds
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                
                var token = tokenHandler.CreateToken(tokenDescripter);

                // passing the token to client
                return Ok(new{
                    token = tokenHandler.WriteToken(token)
                });



            }





    }
}