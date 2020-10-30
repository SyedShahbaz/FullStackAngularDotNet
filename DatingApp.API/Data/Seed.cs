using System.Collections.Generic;
using System.Linq;
using DatingApp.API.Models;
using Newtonsoft.Json;

namespace DatingApp.API.Data
{
    public class Seed
    {
        public static void SeedUsers(DataContext context)
        {
            if (!context.Users.Any())
            {
                var userData = System.IO.File.ReadAllText("Data/UserSeedData.Json");
                var users = JsonConvert.DeserializeObject<List<User>>(userData);

                foreach(var user in users) 
                {
                    byte[] passwordhash, passwordsalt;
                    CreatePasswordHash("password", out passwordhash, out passwordsalt);

                    user.PasswordHash = passwordhash;
                    user.PasswordSalt = passwordsalt;

                    user.Username.ToLower();
                    context.Users.Add(user);
                }
                
                context.SaveChanges();
            }
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var HMAC = new System.Security.Cryptography.HMACSHA512();
            passwordSalt = HMAC.Key;
            passwordHash = HMAC.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }
}