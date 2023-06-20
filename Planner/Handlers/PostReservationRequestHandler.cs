using MediatR;
using Planner.Data;
using Planner.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Planner.Handlers
{
    public class PostReservationRequest : IRequest<Reservation>
    {
        public DateTime Date { get; set; }
        public int PartyPeopleId { get; set; }
    }

    public class PostReservationRequestHandler : IRequestHandler<PostReservationRequest, Reservation>
    {
        private readonly PlannerContext _plannerContext;

        public PostReservationRequestHandler(PlannerContext plannerContext)
        {
            _plannerContext = plannerContext;
        }

        public async Task<Reservation> Handle(PostReservationRequest request, CancellationToken cancellationToken)
        {
            var reservation = _plannerContext.Reservations.Add(new Reservation
            {
                Date = request.Date,
                PartyPeopleId = request.PartyPeopleId
            });

            await _plannerContext.SaveChangesAsync(cancellationToken);

            return reservation.Entity;
        }
    }
}