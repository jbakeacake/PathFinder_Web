using System;
using System.Collections.Generic;
using System.Linq;
using Domain;

namespace Persistence
{
    public static class Seed
    {
        public static void SeedData(DataContext context)
        {
            var hmac = new System.Security.Cryptography.HMACSHA512();
            var hashKey = hmac.Key;
            var hashPass = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes("Password"));

            if (!context.Users_Tbl.Any())
            {
                var dummyUsers = new List<User>
                {
                    new User
                    {
                        Id = Guid.NewGuid(),
                        Username = "Dummy User",
                        PasswordHash = hashPass,
                        PasswordSalt = hashKey
                    }
                };
            }

            hmac.Dispose(); // Free up
        }
    }
}