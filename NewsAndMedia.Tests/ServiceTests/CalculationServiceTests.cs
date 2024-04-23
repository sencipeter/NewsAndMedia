using NewsAndMedia.Core.Interfaces;
using NewsAndMedia.Infrastructure.Services;

namespace NewsAndMedia.Tests.ServiceTests
{
    public class CalculationServiceTests
    {
        ICalculationService _service = new CalculationService();

        [Fact]
        public void CalculateTest()
        {
            var expected = 1.321M;
            var actual = Math.Round(_service.Calculate(10),3);
            Assert.Equal(expected, actual);
        }
    }
}
