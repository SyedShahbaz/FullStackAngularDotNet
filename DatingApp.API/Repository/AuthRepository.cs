using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Models;

namespace DatingApp.API.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _dataContext;

        public AuthRepository(DataContext dataContext)
        {
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }
        public async Task<User> Register(User user, string password)
        {
            CreatePasswordHash(password, out var passwordHash, out var passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            await _dataContext.Users.AddAsync(user);
            await _dataContext.SaveChangesAsync();

            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var HMAC = new System.Security.Cryptography.HMACSHA512();
            passwordSalt = HMAC.Key;
            passwordHash = HMAC.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }

        public async Task<User> Login(string username, string password)
        {
            var user = _dataContext.Users?.FirstOrDefault(u => u.Username == username);

            if (user == null)
                return null;
            return !VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt) ? null : user;
        }

        private bool VerifyPasswordHash(string password, byte[] userPasswordHash, byte[] userPasswordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(userPasswordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                //if (computedHash.Where((t, i) => t != userPasswordHash[i]).Any())
                //{
                //    return false;
                //}

                if (!computedHash.SequenceEqual(userPasswordHash))
                {
                    return false;
                }
            }

            return true;
        }

        public async Task<bool> UserExists(string name)
        {
            return _dataContext.Users.Any(x => x.Username == name);
        }
    }
}
