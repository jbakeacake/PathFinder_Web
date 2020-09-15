using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;

namespace Application.Users
{
    public class Register
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public string Username { get; set; }
            public string PlainTextPassword { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(request.PlainTextPassword, out passwordHash, out passwordSalt);

                var user = new User
                {
                    Id = request.Id,
                    Username = request.Username.ToLower(),
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt
                };

                _context.Users_Tbl.Add(user);
                var res =  await _context.SaveChangesAsync() > 0;

                if(res) return Unit.Value; // A MediatR object that returns a 200 OK response

                throw new Exception($"Problem saving changes on creation of user {request.Id}");
            }

            private void CreatePasswordHash(string plainTextPassword, out byte[] passwordHash, out byte[] passwordSalt)
            {
                using (var hmac = new System.Security.Cryptography.HMACSHA512())
                {
                    passwordSalt = hmac.Key;
                    passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(plainTextPassword));
                }
            }
        }
    }
}