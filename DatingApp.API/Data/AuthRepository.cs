using System;
using System.Threading.Tasks;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    // we impleament our interface we created  
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        //inject DataContext to use our database 
        public AuthRepository(DataContext context)
        {
            _context = context;
        }


        public async Task<User> Login(string username, string password)
        {
            // this method get username and password
            // with the usrname we compare in our db  using (firstOrDefault)
            // and with the password we compute Hash and compare it in our db
            

            // fish out the user from our db using 
            // firstOrDefault will fish the user from our db if its not exist it will be null
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);

            if(user == null){
                //username not exist
                return null; 
            }

            if(!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                //password is incorrect
                return null;
            }

                return user;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            /*we compute the hash of the password we get from user and then we compare
            it using for loop on byte array
            if one of the indexes not match we return false else we return true
            */
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i=0;i<computedHash.Length;i++)
                {
                    if(computedHash[i] != passwordHash[i]){
                        return false;
                    }
                }
            }
            return true;
        }

        public async Task<User> Register(User user, string Password)
        {
           byte[] passwordHash, passwordSalt;
            CreatePasswordHash(Password, out passwordHash, out passwordSalt);

            //pass the password hash and salt to user object
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            //pass the user data to our db
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
           //here we encrypt the password using Salt and Hash
          using(var hmac = new System.Security.Cryptography.HMACSHA512()){
              passwordSalt = hmac.Key;
              passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
          } 
        }
        
        public async Task<bool> UserExists(string username)
        {
            //compare username parameter against all Usernames we have in our db
           if (await _context.Users.AnyAsync(x => x.Username == username)){
               return true;
           }
           return false;
        }
    }
}