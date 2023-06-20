using MediatR;
using Planner.Data;
using Planner.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Planner.Handlers
{
    public class DeleteReservationRequest : IRequest<Task>
    {
        public int ReservationId { get; set; }
    }

    public class DeleteReservationRequestHandler : IRequestHandler<DeleteReservationRequest, Task>
    {
        private readonly PlannerContext _plannerContext;

        public DeleteReservationRequestHandler(PlannerContext plannerContext)
        {
            _plannerContext = plannerContext;
        }

        public async Task<Task> Handle(DeleteReservationRequest request, CancellationToken cancellationToken)
        {
            _plannerContext.Reservations.Remove(new Reservation
            {
                Id = request.ReservationId
            });
            await _plannerContext.SaveChangesAsync(cancellationToken);

            return Task.CompletedTask;
        }
    }
}
