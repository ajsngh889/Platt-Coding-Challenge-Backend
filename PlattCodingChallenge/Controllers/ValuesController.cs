using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using PlattCodingChallenge.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PlattCodingChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet("GetAllPlanets")]
        public IActionResult GetAllPlanets()
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

        ////https://localhost:5001/api/Values?planetid=22
        [HttpGet("GetPlanetById/{planetid}")]
        public ActionResult GetPlanetById(int planetid)
        {
            var model = new SinglePlanetViewModel();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://swapi.dev/api/");
                var responseTask = client.GetAsync("planets/" + planetid);
                responseTask.Wait();

                //To store result of web api response.   
                HttpResponseMessage result = responseTask.Result;

                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    string readTask = result.Content.ReadAsStringAsync().Result;
                    model = JsonConvert.DeserializeObject<SinglePlanetViewModel>(readTask);
                }
            }

            return Ok(model);
        }
        
        [HttpGet("GetResidentsOfPlanet/{planetname}")]
        // [Route("{GetResidentsOfPlanet}")]
        public ActionResult GetResidentsOfPlanet(string planetname)
        {
            var model = new MultiplePlanetViewModel();
            MultiplePlanetModel multiplePlanetModel = new MultiplePlanetModel();
            var planetResidentsViewModel = new PlanetResidentsViewModel();
            PlanetResidentModel planetResidentModel = new PlanetResidentModel();
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
                    multiplePlanetModel = JsonConvert.DeserializeObject<MultiplePlanetModel>(readTask);
                    var residents = multiplePlanetModel.Results.Where(x => x.Name == planetname).ToList();
                    var planetDetailsViewModel = new PlanetDetailsViewModel();

                    AllPlanetModel allPlanetModel = new AllPlanetModel();
                    var model2 = new PlanetResidentsViewModel();
                    foreach (var res in residents)
                    {
                        foreach (var res2 in res.Residents)
                        {
                            var path = res2.Split("https://swapi.dev/api/")[1];
                            responseTask = client.GetAsync(path);
                            responseTask.Wait();
                            result = responseTask.Result;
                            if (result.IsSuccessStatusCode)
                            {
                                readTask = result.Content.ReadAsStringAsync().Result;
                                var residentInfo = JsonConvert.DeserializeObject<ResidentSummary>(readTask);
                                planetResidentsViewModel.Residents.Add(residentInfo);
                            }
                        }
                    }
                }
            }
            return Ok(planetResidentsViewModel);
        }


        [HttpGet("VehicleSummary")]
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
