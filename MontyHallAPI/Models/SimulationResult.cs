
using System;

namespace MontyHallAPI.Models
{
    public class SimulationResult
    {
        public decimal AvgWins{get;set;}
        public decimal percentageOfWins{get;set;} 
        public decimal AvgLosses{get;set;} 
        public int NumberOfDoors{get;set;}
        public int NumberOfRunsPerTrial { get; set; }
        public int NumberOfSimulations { get; set; }

    }
}