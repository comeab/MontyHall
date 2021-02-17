using NUnit.Framework;
using MontyHallAPI.Services;

namespace MontyHall.UnitTests.Services
{
    [TestFixture]
    public class SimulationServiceTests
    {
        private SimulationService _simulationService;

        [SetUp]
        public void SetUp()
        {
            _simulationService = new SimulationService();
        }

        [TestCase(20)]
        [TestCase(50)]
        [TestCase(100)]
        [TestCase(1000)]
        [TestCase(1000000)]
        public void RunSimulation_IsSwitchingFalse_ReturnsLessOrEqualThanOneThird(int numberOfSimulations)
        {
            var avg = 0;
            for(int i=0; i<=10; i++){
                avg+=_simulationService.RunSimulation(numberOfSimulations, false, 3);
            }
            //on average should be bellow 40%
            Assert.LessOrEqual(avg/10, (decimal)(numberOfSimulations*0.4));
        }

        [TestCase(20)]
        [TestCase(50)]
        [TestCase(100)]
        [TestCase(1000)]
        [TestCase(1000000)]
        public void RunSimulation_IsSwitchingTrue_ReturnsLessOrEqualThanOneThird(int numberOfSimulations)
        {
            var avg = 0;
            for(int i=0; i<=10; i++){
                avg+=_simulationService.RunSimulation(numberOfSimulations, true, 3);
            }
            //on average should be minimum 60%
            Assert.GreaterOrEqual(avg/10, numberOfSimulations*0.6);
        }
    }
}