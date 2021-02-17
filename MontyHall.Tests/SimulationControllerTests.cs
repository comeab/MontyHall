
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

        [Test]
        public void Post_Game_ReturnsSimulationResults(){
            var mockSimulationService = new Mock<ISimulationService>();
            mockSimulationService.Setup(service => service.RunSimulation(It.IsAny<int>(), It.IsAny<Boolean>(), It.IsAny<int>())).Returns(60);
            var sut = new SimulationController(mockSimulationService.Object);

            var result = sut.Post(new Game(){ NumberOfSimulations = 100, IsSwitching = true }) as OkObjectResult;
            Assert.IsInstanceOf<OkObjectResult>(result);
           
            var simulationResult = result.Value as SimulationResult;
            Assert.AreEqual(simulationResult.NumberOfDoors, 3);
            Assert.AreEqual(simulationResult.numberOfWins, 60);
            Assert.AreEqual(simulationResult.numberOfLosses, 40);
            Assert.AreEqual(simulationResult.percentageOfWins, 0.6);
        }

        
        [Test]
        public void Post_NrSimulationsZero_Returns400(){
            var mockSimulationService = new Mock<ISimulationService>();
            mockSimulationService.Setup(repo => repo.RunSimulation(It.IsAny<int>(), It.IsAny<Boolean>(), It.IsAny<int>())).Returns(10);
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