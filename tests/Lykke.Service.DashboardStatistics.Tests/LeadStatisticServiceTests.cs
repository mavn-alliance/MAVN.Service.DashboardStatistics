using System;
using System.Collections.Generic;
using System.Linq;
using Lykke.Service.DashboardStatistics.Domain.Models.LeadStatistic;
using Lykke.Service.DashboardStatistics.Domain.Repositories;
using Lykke.Service.DashboardStatistics.DomainServices;
using Moq;
using Xunit;

namespace Lykke.Service.DashboardStatistics.Tests
{
    public class LeadStatisticServiceTests
    {
        [Fact]
        public async void ShouldReturnCorrectUniqueValues_WhenMultipleDaysAndOverlappingStates()
        {
            // Arrange
            var from = new DateTime(2019, 8, 5);
            var to = new DateTime(2019, 8, 6);
            var lead1 = Guid.NewGuid().ToString();
            var lead2 = Guid.NewGuid().ToString();
            var lead3 = Guid.NewGuid().ToString();
            var lead4 = Guid.NewGuid().ToString();
            var data = new List<LeadModel>
            {
                new LeadModel {LeadId = lead1, TimeStamp = from, State = LeadState.Pending},
                new LeadModel {LeadId = lead2, TimeStamp = from, State = LeadState.Pending},
                new LeadModel {LeadId = lead2, TimeStamp = from, State = LeadState.Confirmed},
                new LeadModel {LeadId = lead2, TimeStamp = to, State = LeadState.Approved},
                new LeadModel {LeadId = lead3, TimeStamp = to, State = LeadState.Pending},
                new LeadModel {LeadId = lead3, TimeStamp = to, State = LeadState.Confirmed},
                new LeadModel {LeadId = lead3, TimeStamp = to, State = LeadState.Approved},
                new LeadModel {LeadId = lead4, TimeStamp = to, State = LeadState.Pending},
                new LeadModel {LeadId = lead4, TimeStamp = to, State = LeadState.Confirmed},
            };
            
            var mockedRepo = new Mock<ILeadStatisticRepository>(MockBehavior.Strict);
            mockedRepo.Setup(r => r.GetAsync(It.IsAny<DateTime>(), It.IsAny<DateTime>())).ReturnsAsync(data);

            var service = new LeadStatisticService(mockedRepo.Object);

            // Act
            var result = await service.GetAsync(from, to);

            // Assert
            Assert.Equal(4, result.TotalCount);
            
            var day1 = result.LeadsByDate.First();
            Assert.Equal(1, day1.Value.First(v => v.State == LeadState.Pending).Count);
            Assert.Equal(1, day1.Value.First(v => v.State == LeadState.Confirmed).Count);
            Assert.Equal(0, day1.Value.First(v => v.State == LeadState.Approved).Count);
            Assert.Equal(2, day1.Total);

            var day2 = result.LeadsByDate.Skip(1).First();
            Assert.Equal(1, day2.Value.First(v => v.State == LeadState.Pending).Count);
            Assert.Equal(1, day2.Value.First(v => v.State == LeadState.Confirmed).Count);
            Assert.Equal(2, day2.Value.First(v => v.State == LeadState.Approved).Count);
            Assert.Equal(4, day2.Total);
        }
    }
}
