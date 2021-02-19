using System;
using System.Collections.Generic;
using System.Linq;
using MontyHallAPI.Models;

namespace MontyHallAPI.Services
{
    public class SimulationService : ISimulationService
    {
        private enum DoorPrizeType { GOAT, CAR }
        private int seed = 1; 
        private Boolean HasUserWon(DoorPrizeType userFirstChoice, Boolean isSwitching)
        {
            if (!isSwitching) return (userFirstChoice == DoorPrizeType.CAR);
            else return userFirstChoice == DoorPrizeType.GOAT;
        }

        private List<DoorPrizeType> CreateListOfDoorsWithOneCarOnly(int numberOfDoors)
        {
            Random rnd = new Random();
            var doors = new List<DoorPrizeType>();
            doors.AddRange(Enumerable.Repeat(DoorPrizeType.GOAT, numberOfDoors));
            doors[rnd.Next(doors.Count)] = DoorPrizeType.CAR;

            return doors;
        }

        private void Shuffle(List<DoorPrizeType> doors)
        {
            doors.OrderBy(x => new Random().Next());
        }

        public SimulationResult RunSimulation(int numberOfSimulations, Boolean isSwitching, int numberOfDoors = 3)
        {
            var doors = CreateListOfDoorsWithOneCarOnly(numberOfDoors);
            Random rnd = new Random();
            decimal counterOfWins = 0;

            for (int i = 0; i < numberOfSimulations; i++)
            {
                Shuffle(doors);
                int userRandomDoor = rnd.Next(doors.Count);
                var userFirstChoice = doors[userRandomDoor];
                if (HasUserWon(userFirstChoice, isSwitching)) counterOfWins++;
            }

            var wins = counterOfWins;
            var percentageOfWins = wins / numberOfSimulations;
            var lossess = numberOfSimulations - wins;
            
            return new SimulationResult()
            {
                wins = wins,
                percentageOfWins = percentageOfWins,
                losses = lossess,
                NumberOfDoors = numberOfDoors,
                NumberOfSimulations = numberOfSimulations
            };
        }

    }

}