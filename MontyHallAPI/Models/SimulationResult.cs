
using System;

namespace MontyHallAPI.Models
{
    public class SimulationResult
    {
        public decimal wins{get;set;}
        public decimal percentageOfWins{get;set;} 
        public decimal losses{get;set;} 
        public int NumberOfDoors{get;set;}
        public int NumberOfSimulations { get; set; }

    }
}