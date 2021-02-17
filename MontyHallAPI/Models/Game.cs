
using System;

namespace MontyHallAPI.Models
{
    public class Game
    {
        
        public int NumberOfSimulations { get; set; }
        public Boolean IsSwitching { get; set; }
        public int NumberOfDoors { get; } = 3;

    }
}