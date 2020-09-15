using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Users
{
    public class Login
    {
        public class Query : IRequest<User>
        {
            public string Username { get; set; }
            public string PlaintextPassword { get; set; }
        }

        public class Handler : IRequestHandler<Query, User>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<User> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await _context.Users_Tbl.FirstOrDefaultAsync(x => x.Username.ToLower() == request.Username.ToLower());
                if(user == null)
                    return null;
                if(!VerifyPasswordHash(request.PlaintextPassword, user.PasswordHash, user.PasswordSalt))
                    return null;
                
                return user;
            }

            private bool VerifyPasswordHash(string plaintextPassword, byte[] passwordHash, byte[] passwordSalt)
            {
                using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
                {
                    var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(plaintextPassword));
                    for(int i = 0; i < computedHash.Length; i++)
                    {
                        if(computedHash[i] != passwordHash[i]) return false;
                    }
                }
                return true;
            }
        }
    }
}