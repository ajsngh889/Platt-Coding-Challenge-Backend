using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlattCodingChallenge.Models
{
    public class MultiplePlanetModel
    {
        public int Count { get; set; }
        public string Next { get; set; }
        public string Previous { get; set; }

        public List<MultiplePlanetViewModel> Results { get; set; }
    }
 
}
