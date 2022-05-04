using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlattCodingChallenge.Models
{
    public class MultiplePlanetViewModel
    {
        public string Name { get; set; }

        public string Rotation_period { get; set; }

        public string LengthOfYear { get; set; }

        public string Diameter { get; set; }

        public string Climate { get; set; }

        public string Gravity { get; set; }

        public string Surface_water { get; set; }

        public string Population { get; set; } = "0";
        public List<string> Residents { get; set; }
        public List<string> Films { get; set; }
        public DateTime Created { get; set; }
        public DateTime Edited { get; set; }
        public string Url { get; set; }
    }
 
}
