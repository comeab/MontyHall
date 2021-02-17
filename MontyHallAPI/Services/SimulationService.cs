using System;
using System.Collections.Generic;
using System.Linq;

namespace MontyHallAPI.Services
{
    public class SimulationService : ISimulationService
    {
        private enum DoorPrizeType { GOAT, CAR }
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
        public int RunSimulation(int numberOfSimulations, Boolean isSwitching, int numberOfDoors = 3)
        {
            var doors = CreateListOfDoorsWithOneCarOnly(numberOfDoors); // []
            int counterOfWins = 0;
            Random rnd = new Random();

            for (int i = 0; i < numberOfSimulations; i++)
            {
                Shuffle(doors);
                int userRandomDoor = rnd.Next(doors.Count);
                var userFirstChoice = doors[userRandomDoor];

                if (HasUserWon(userFirstChoice, isSwitching)) counterOfWins++;
            }

            return counterOfWins;
        }

    }

}