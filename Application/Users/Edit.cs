using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistence;

namespace Application.Users
{
    public class Edit
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public string Username { get; set; }
            public string PlaintextPassword { get; set; }
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
                var userToEdit = await _context.Users_Tbl.FindAsync(request.Id);

                if (userToEdit == null)
                    throw new Exception("Could not find activity");

                userToEdit.Username = request.Username ?? userToEdit.Username;
                
                byte[] passwordHash = null, passwordSalt = null;

                if(!String.IsNullOrEmpty(request.PlaintextPassword))
                    CreatePasswordHash(request.PlaintextPassword.Trim(), out passwordHash, out passwordSalt);
                
                userToEdit.PasswordHash = passwordHash ?? userToEdit.PasswordHash;
                userToEdit.PasswordSalt = passwordSalt ?? userToEdit.PasswordSalt;

                var res = await _context.SaveChangesAsync() > 0;
                
                if(res) return Unit.Value;

                throw new Exception($"Problem saving changes for user: {request.Id}");
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