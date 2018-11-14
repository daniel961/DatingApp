namespace DatingApp.API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }

        // for the security we using password hash and password salt 
        // password salt act like key helps us to recreate the Hash
        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }


    }
}