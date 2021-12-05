using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;

namespace Application.Bikes
{
    public class Edit
    {
        public class Command : IRequest
        {
            public Bike Bike { get; set; }
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
                await ((IBikesRepository)_repository).UpdateBikeAsync(request.Bike);
                return Unit.Value;
            }
        }
    }
}