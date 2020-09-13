using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;

namespace Application.Users
{
    public class Create
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public string Username { get; set; }
            public byte[] PasswordHash { get; set; }
            public byte[] PasswordSalt { get; set; }
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
                var user = new User
                {
                    Id = request.Id,
                    Username = request.Username,
                    PasswordHash = request.PasswordHash,
                    PasswordSalt = request.PasswordSalt
                };

                _context.Users_Tbl.Add(user);
                var res =  await _context.SaveChangesAsync() > 0;

                if(res) return Unit.Value; // A MediatR object that returns a 200 OK response

                throw new Exception($"Problem saving changes on creation of user {request.Id}");
            }
        }
    }
}