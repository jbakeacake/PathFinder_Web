using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistence;

namespace Application.Users
{
    public class Delete
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
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
                var userToDelete = await _context.Users_Tbl.FindAsync(request.Id);

                if(userToDelete == null)
                    throw new Exception("Could not find activity");
                
                _context.Remove(userToDelete);

                var res = await _context.SaveChangesAsync() > 0;

                if (res) return Unit.Value;

                throw new Exception($"Problem saving changes when deleteing user: {request.Id}");
            }
        }
    }
}