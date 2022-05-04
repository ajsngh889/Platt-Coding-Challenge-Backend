using System.Collections.Generic;

namespace PlattCodingChallenge.Models
{
    public class PlanetResidentModel
    {
        public int Count { get; set; }
        public string Next { get; set; }
        public string Previous { get; set; }

        public List<ResidentSummary> Results { get; set; }
    }
}
