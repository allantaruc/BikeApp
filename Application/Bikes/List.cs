using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Bikes
{
    public class List
    {
        public class Query : IRequest<List<Bike>> { }

        public class Handler : IRequestHandler<Query, List<Bike>>
        {
            // private readonly DataContext _context;
            // public Handler(DataContext context)
            // {
            //     _context = context;
            // }

            private readonly IRepository _repository;
            public Handler(IRepository repository)
            {
                _repository = repository;
            }

            public async Task<List<Bike>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await ((IBikesRepository)_repository).GetBikesAsync();
            }
        }
    }
}