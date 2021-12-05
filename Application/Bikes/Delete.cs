using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistence;

namespace Application.Bikes
{
    public class Delete
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IRepository _repository;
            public Handler(IRepository repository)
            {
                _repository = repository;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var bikeFromDb = await ((IBikesRepository)_repository).GetBikeAsync(request.Id);
                await ((IBikesRepository)_repository).DeleteBikeAsync(bikeFromDb);
                return Unit.Value;
            }
        }
    }
}