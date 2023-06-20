using MediatR;
using Planner.Data;
using Planner.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Planner.Handlers
{
    public class PutReservationRequest : IRequest<Reservation>
    {
        public DateTime Date { get; set; }
        public int PartyPeopleId { get; set; }
        public int ReservationId { get; set; }
    }

    public class PutReservationRequestHandler : IRequestHandler<PutReservationRequest, Reservation>
    {
        private readonly PlannerContext _plannerContext;

        public PutReservationRequestHandler(PlannerContext plannerContext)
        {
            _plannerContext = plannerContext;
        }

        public async Task<Reservation> Handle(PutReservationRequest request, CancellationToken cancellationToken)
        {
            var result = _plannerContext.Reservations.Update(new Reservation
            {
                Date = request.Date,
                Id = request.ReservationId,
                PartyPeopleId = request.PartyPeopleId
            });
            await _plannerContext.SaveChangesAsync(cancellationToken);

            return result.Entity;
        }
    }
}
