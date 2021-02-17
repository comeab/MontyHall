using System;
using Microsoft.AspNetCore.Mvc;
using MontyHallAPI.Models;
using MontyHallAPI.Services;

namespace MontyHallAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SimulationController : Controller
    {
        private readonly ISimulationService _simulationService;
        public SimulationController(ISimulationService simulationService)
        {
            _simulationService = simulationService;
        }

        [HttpPost]
        public ActionResult Post([FromBody] Game game)
        {
            try
            {
                if (game.NumberOfSimulations <= 0) return BadRequest("Number of simulations cannot be less or equal to zero");

                var numberOfWins = _simulationService.RunSimulation(game.NumberOfSimulations, game.IsSwitching, game.NumberOfDoors);
                var percentageOfWins = (decimal)(numberOfWins / game.NumberOfSimulations);
                var numberOfLosses = game.NumberOfSimulations - numberOfWins;

                return Ok(new SimulationResult
                {
                    numberOfWins = numberOfWins,
                    percentageOfWins = percentageOfWins,
                    numberOfLosses = numberOfLosses,
                    NumberOfDoors = game.NumberOfDoors
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Something went wrong: {ex.Message}");
            }


        }
    }
}