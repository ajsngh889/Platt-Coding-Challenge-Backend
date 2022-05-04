using System.Collections.Generic;

namespace PlattCodingChallenge.Models
{
    public class AllPlanetModel
    {
        public int count { get; set; }
        public string next { get; set; }
        public string previous { get; set; }
        public List<PlanetDetailsViewModel> Results { get; set; }

    }
}
