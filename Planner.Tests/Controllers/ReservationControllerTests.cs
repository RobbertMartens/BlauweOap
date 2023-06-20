using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Planner.Controllers;
using Planner.Handlers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Planner.Tests.Controllers
{
    [TestClass]
    public class ReservationControllerTests
    {
        private Mock<ILogger<ReservationsController>> _loggerMock;
        private Mock<IMediator> _mediatorMock;

        [TestInitialize]
        public void TestInitialize()
        {
            _loggerMock = new Mock<ILogger<ReservationsController>>();
            _mediatorMock = new Mock<IMediator>();

        }

        [TestMethod]
        public async Task Get_UsingValidParameters_Returns200()
        {
            var mediatorResult = new List<object>
            {
                new { Date = DateTime.Now.AddMonths(1), Name = "test" },
                new { Date = DateTime.Now.AddMonths(2), Name = "test" }
            };
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetReservationRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(mediatorResult);

            var reservationsController = new ReservationsController(_loggerMock.Object, _mediatorMock.Object);

            var result = await reservationsController.Get(1);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }
    }
}