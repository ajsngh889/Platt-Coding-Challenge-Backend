using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PlattCodingChallenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PlattCodingChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SWA : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllPlanets1()
        {
            AllPlanetsViewModel model = new AllPlanetsViewModel();
            AllPlanetModel allPlanetModel = new AllPlanetModel();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://swapi.dev/api/");
                var responseTask = client.GetAsync("planets");
                responseTask.Wait();

                //To store result of web api response.   
                HttpResponseMessage result = responseTask.Result;

                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    string readTask = result.Content.ReadAsStringAsync().Result;
                    allPlanetModel = JsonConvert.DeserializeObject<AllPlanetModel>(readTask);
                    model.Planets = allPlanetModel.Results.OrderByDescending(d => d.Diameter).ToList();
                }
            }

            return Ok(allPlanetModel);
        }

		[HttpGet("[action]/{VehicleSummary}")]
		public IActionResult VehicleSummary()
		{
			var model = new VehicleSummaryViewModel();
			VehicleModel vehicleModel = new VehicleModel();
			// TODO: Implement this controller action

			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri("https://swapi.dev/api/");
				var responseTask = client.GetAsync("vehicles");
				responseTask.Wait();

				//To store result of web api response.   
				HttpResponseMessage result = responseTask.Result;

				//If success received   
				if (result.IsSuccessStatusCode)
				{
					string readTask = result.Content.ReadAsStringAsync().Result;
					vehicleModel = JsonConvert.DeserializeObject<VehicleModel>(readTask);

					var x = vehicleModel.results
						.Where(x => x.cost_in_credits != "unknown");

					//IQueryable
					var distictManufactureres = x.GroupBy(x => x.manufacturer);

					List<VehicleStatsViewModel> lstVehicleStatsViewModel = distictManufactureres
						.Select(z => new VehicleStatsViewModel
						{
							ManufacturerName = z.Select(x => x.manufacturer).FirstOrDefault(),
							AverageCost = z.Sum(x => Convert.ToInt32(x.cost_in_credits)),
							VehicleCount = z.Select(x => x.manufacturer).Count()
						}

						).ToList();

					model.VehicleCount = x.Count();
					model.ManufacturerCount = distictManufactureres.Count();
					model.Details = lstVehicleStatsViewModel;

				}
			}

			return Ok(model);
		}


	}
}
