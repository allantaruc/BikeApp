using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;

namespace Application.Bikes
{
    public class Details
    {
        public class Query : IRequest<Bike>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Bike>
        {
            //private readonly DataContext _context;
            private readonly IRepository _repository;
            public Handler(IRepository repository)
            {
                _repository = repository;
            }

            public async Task<Bike> Handle(Query request, CancellationToken cancellationToken)
            {
                // return await _context.Bikes.FindAsync(request.Id);
                return await ((IBikesRepository)_repository).GetBikeAsync(request.Id);
            }
        }
    }
}