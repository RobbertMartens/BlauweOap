using System;

namespace Planner.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int PartyPeopleId { get; set; }
    }
}
