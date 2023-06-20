using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Planner.Models;
using System;
using System.Linq;

namespace Planner.Data
{
    public static class DbInitializer
    {
        public static void CreateDbIfNotExists(WebApplication app)
        {
            try
            {
                var context = app.Services.GetService<PlannerContext>();
                Initialize(context);
            }
            catch (Exception ex)
            {
                var logger = app.Services.GetService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred creating the DB.");
            }
        }

        private static void Initialize(PlannerContext context)
        {
            context.Database.EnsureCreated();

            if (context.PartyPeople.Any())
            {
                return;
            }

            var partyPeople = new PartyPeople[]
            {
                new PartyPeople{ Name = "Bassie", Email = "bas.van.der.tor@test.nl"},
                new PartyPeople{ Name = "Adriaan", Email = "aad.van.der.tor@test.nl"},
                new PartyPeople{ Name = "B100", Email = "B100@test.nl"},
                new PartyPeople{ Name = "Vlugge Japie", Email = "jaapdedraak@test.nl"},
            };

            foreach (var pp in partyPeople)
            {
                context.PartyPeople.Add(pp);
            }
            context.SaveChanges();

            var reservations = new Reservation[]
            {
                new Reservation{ Date = DateTime.Now.AddMonths(1), PartyPeopleId = 1},
                new Reservation{ Date = DateTime.Now.AddMonths(2), PartyPeopleId = 2},
                new Reservation{ Date = DateTime.Now.AddMonths(3), PartyPeopleId = 3},
                new Reservation{ Date = DateTime.Now.AddMonths(4), PartyPeopleId = 1},
            };
            foreach (var reservation in reservations)
            {
                context.Reservations.Add(reservation);
            }
            context.SaveChanges();
        }
    }
}
