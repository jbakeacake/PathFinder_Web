using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;

namespace Application.Users
{
    public class Details
    {
        public class Query : IRequest<User>
        {
            public Guid Id { get; set; }
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
                if(!_context.Users_Tbl.Any(x => x.Id == request.Id))
                    throw new Exception("Unable to find user");

                var user = await _context.Users_Tbl.FindAsync(request.Id);
                return user;
            }
        }
    }
}