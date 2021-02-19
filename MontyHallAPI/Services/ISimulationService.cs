using System;
using MontyHallAPI.Models;

namespace MontyHallAPI.Services
{
    public interface ISimulationService
    {
        public SimulationResult RunSimulation(int numberOfSimulations, Boolean isSwitching, int numberOfDoors);
    }

}