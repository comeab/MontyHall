
using NUnit.Framework;
using Moq;
using MontyHallAPI.Controllers;
using MontyHallAPI.Models;
using MontyHallAPI.Services;
using System;
using Microsoft.AspNetCore.Mvc;

namespace MontyHallTests.UnitTests.Controllers{
    [TestFixture]
    public class SimulationControllerTests{

        [TestCase(100, 60)]
        [TestCase(100, 30)]
        [TestCase(1000000, 910000)]
        public void Post_Game_ReturnsSimulationResults(int NumberOfSimulations, int nrWins){
            
            var simulationResultObj = new SimulationResult() {
                AvgWins = nrWins,
                percentageOfWins = (decimal)(nrWins/NumberOfSimulations), 
                AvgLosses = NumberOfSimulations - nrWins,
                NumberOfDoors = 3,
                NumberOfRunsPerTrial = 1000
            };
            var mockSimulationService = new Mock<ISimulationService>();
            mockSimulationService.Setup(service => service.RunSimulation(It.IsAny<int>(), It.IsAny<Boolean>(), It.IsAny<int>())).Returns(simulationResultObj);
            var sut = new SimulationController(mockSimulationService.Object);

            var result = sut.Post(new Game(){ NumberOfSimulations = NumberOfSimulations }) as OkObjectResult;
            Assert.IsInstanceOf<OkObjectResult>(result);
           
            var simulationResult = result.Value as SimulationResult;
            Assert.GreaterOrEqual(simulationResult.AvgWins, nrWins);
            Assert.GreaterOrEqual(simulationResult.AvgLosses, simulationResultObj.AvgLosses );
            Assert.GreaterOrEqual(simulationResult.percentageOfWins, simulationResultObj.percentageOfWins);
            Assert.AreEqual(simulationResult.NumberOfDoors, simulationResultObj.NumberOfDoors);
            Assert.AreEqual(simulationResult.NumberOfRunsPerTrial, simulationResultObj.NumberOfRunsPerTrial);
        }

        
        [Test]
        public void Post_NumberOfSimulationsIsZero_Returns400(){
            var mockSimulationService = new Mock<ISimulationService>();

            var sut = new SimulationController(mockSimulationService.Object);
            var result = sut.Post(new Game(){NumberOfSimulations = 0 }) as BadRequestObjectResult;
            
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var simulationResult = result.Value as SimulationResult;

            Assert.IsNull(simulationResult);
        }

         [Test]
        public void Post_throwsException_Returns500(){

            var mockSimulationService = new Mock<ISimulationService>();
            mockSimulationService.Setup(repo => repo.RunSimulation(It.IsAny<int>(), It.IsAny<Boolean>(), It.IsAny<int>())).Throws(new Exception("Random exception"));
            var sut = new SimulationController(mockSimulationService.Object);

            var result = sut.Post(new Game(){NumberOfSimulations = 10 }) as ObjectResult;
            Assert.AreEqual(result.StatusCode, 500);

            var simulationResult = result.Value as SimulationResult;
            Assert.IsNull(simulationResult);
        }
    }
}