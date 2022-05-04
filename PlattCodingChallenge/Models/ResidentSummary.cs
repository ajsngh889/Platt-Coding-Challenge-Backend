using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlattCodingChallenge.Models
{
	public class ResidentSummary
	{
        public string Name { get; set; }
        public string Height { get; set; }
        public string Mass { get; set; }
        public string Hair_Color { get; set; }
        public string Skin_Color { get; set; }
        public string Eye_Color { get; set; }
        public string Birth_Year { get; set; }
        public string Gender { get; set; }
        public string Homeworld { get; set; }
        public List<string> Films { get; set; }
        public List<object> Species { get; set; }
        public List<string> Vehicles { get; set; }
        public List<object> Starships { get; set; }
        public DateTime Created { get; set; }
        public DateTime Edited { get; set; }
        public string Url { get; set; }
	}
}
