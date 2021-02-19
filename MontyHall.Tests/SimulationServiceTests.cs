using NUnit.Framework;
using MontyHallAPI.Services;
using MontyHallAPI.Models;

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
        public void RunSimulation_IsSwitchingFalse_ReturnsLessOrEqualTo40Percent(int numberOfSimulations)
        {
            var simulationResult = _simulationService.RunSimulation(numberOfSimulations, false);
            //Upper bound is 40%
            Assert.LessOrEqual(simulationResult.percentageOfWins, 0.4);
            Assert.IsInstanceOf<SimulationResult>(simulationResult);
        }

        [TestCase(20)]
        [TestCase(50)]
        [TestCase(100)]
        [TestCase(1000)]
        [TestCase(1000000)]
        public void RunSimulation_IsSwitchingTrue_ReturnsMoreOrEqualTo60Percent(int numberOfSimulations)
        {
            var simulationResult = _simulationService.RunSimulation(numberOfSimulations, true);
            //Lower bound is 60%
            Assert.GreaterOrEqual(simulationResult.percentageOfWins, 0.6);

            Assert.IsInstanceOf<SimulationResult>(simulationResult);
        }
    }
}