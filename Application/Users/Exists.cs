using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Users
{
    public class Exists
    {
        public class Query : IRequest<bool>
        {
            public Guid Id { get; set; }
            public string Username { get; set; }
        }
        public class Handler : IRequestHandler<Query, bool>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<bool> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Users_Tbl.AnyAsync(x => x.Username == request.Username);
            }
        }
    }
}