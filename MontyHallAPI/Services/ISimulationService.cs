using System;
namespace MontyHallAPI.Services
{
    public interface ISimulationService
    {
        public int RunSimulation(int numberOfSimulations, Boolean isSwitching, int numberOfDoors);
    }

}