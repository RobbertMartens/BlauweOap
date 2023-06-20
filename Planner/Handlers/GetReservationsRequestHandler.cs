using MediatR;
using Microsoft.EntityFrameworkCore;
using Planner.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Planner.Handlers
{
    public class GetReservationRequest : IRequest<IEnumerable<object>>
    {
        public int PartyPeopleId { get; set; }
    }

    public class GetReservationsRequestHandler : IRequestHandler<GetReservationRequest, IEnumerable<object>>
    {
        private readonly PlannerContext _plannerContext;

        public GetReservationsRequestHandler(PlannerContext plannerContext)
        {
            _plannerContext = plannerContext;
        }

        public async Task<IEnumerable<object>> Handle(GetReservationRequest request, CancellationToken cancellationToken)
        {
            return await (from pp in _plannerContext.PartyPeople
                         join r in _plannerContext.Reservations on pp.Id equals r.PartyPeopleId
                         where r.PartyPeopleId == request.PartyPeopleId
                         select new { pp.Name, r.Date, r.Id }).ToListAsync(cancellationToken);
        }
    }
}
