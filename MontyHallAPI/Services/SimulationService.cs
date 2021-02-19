using System;
using System.Collections.Generic;
using System.Linq;
using MontyHallAPI.Models;

namespace MontyHallAPI.Services
{
    public class SimulationService : ISimulationService
    {
        private enum DoorPrizeType { GOAT, CAR }
        private int NumberOfRunsPerTrial = 1; 
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

        private int RunTrial(int numberOfSimulations, Boolean isSwitching, List<DoorPrizeType> doors){
            int counterOfWinsPerTrial = 0;
            Random rnd = new Random();
            for (int j = 0; j < numberOfSimulations; j++)
            {
                Shuffle(doors);
                int userRandomDoor = rnd.Next(doors.Count);
                var userFirstChoice = doors[userRandomDoor];

                if (HasUserWon(userFirstChoice, isSwitching)) counterOfWinsPerTrial++;
            }
            return counterOfWinsPerTrial;
        }
        public SimulationResult RunSimulation(int numberOfSimulations, Boolean isSwitching, int numberOfDoors = 3)
        {
            var doors = CreateListOfDoorsWithOneCarOnly(numberOfDoors);
           
            decimal counterTotalWins = 0;
            for (int i = 0; i < NumberOfRunsPerTrial; i++)
            {
                int counterOfWinsPerTrial = RunTrial(numberOfSimulations, isSwitching, doors);
                counterTotalWins += counterOfWinsPerTrial;
            }

            var avgWins = counterTotalWins / NumberOfRunsPerTrial;
            var percentageOfWins = avgWins / numberOfSimulations;
            var numberOfLosses = numberOfSimulations - avgWins;
            
            return new SimulationResult()
            {
                AvgWins = avgWins,
                percentageOfWins = percentageOfWins,
                AvgLosses = numberOfLosses,
                NumberOfDoors = numberOfDoors,
                NumberOfRunsPerTrial = NumberOfRunsPerTrial,
                NumberOfSimulations = numberOfSimulations
            };
        }

    }

}