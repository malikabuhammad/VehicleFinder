using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Newtonsoft.Json;
using System.Net.Http;
using VehicleFinder.Models;
using static VehicleFinder.Models.HomeModels;

namespace VehicleFinder.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;

        public HomeController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetCarMakes(string searchTerm = "", int page = 1)
        {
            try
            {
                // Define the number of items per page (100 in this case)
                int pageSize = 100;

                // Fetch data from the API
                var makesResponse = await _httpClient.GetStringAsync("https://vpic.nhtsa.dot.gov/api/vehicles/getallmakes?format=json");
                var makeData = JsonConvert.DeserializeObject<MakeResult>(makesResponse);

                // Filter the data based on the search term or return the first 100 records if no search term is provided
                var filteredMakes = makeData.Results
                    .Where(m => string.IsNullOrEmpty(searchTerm) || m.Make_Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                    .Skip((page - 1) * pageSize) // Pagination
                    .Take(pageSize)
                    .Select(r => new { id = r.Make_ID, text = r.Make_Name })
                    .ToList();

                // Return results with pagination data
                var result = new
                {
                    results = filteredMakes,
                    pagination = new { more = filteredMakes.Count == pageSize } // Check if there are more results
                };

                return Json(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }




        [HttpGet]
        public async Task<IActionResult> GetVehicleTypeByMakeID(string MakeID)
        {
            try
            {
              
                var vehcileResponse = await _httpClient.GetStringAsync($"https://vpic.nhtsa.dot.gov/api/vehicles/GetVehicleTypesForMakeId/{MakeID}?format=json");
                var typeData = JsonConvert.DeserializeObject<VechicleTypeResult>(vehcileResponse);



                 var result = typeData.Results.Select(r => new
                {
                    id = r.VehicleTypeId,
                    name=r.VehicleTypeName
                });
                return Json(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetCars(string MakeID, string Year )
        {
            try
            {
                // Define the number of items per page (100 in this case)
                int pageSize = 100;

                // Fetch data from the API
                var vehcileResponse = await _httpClient.GetStringAsync($"https://vpic.nhtsa.dot.gov/api/vehicles/GetModelsForMakeIdYear/makeId/{MakeID}/modelyear/{Year}?format=json");
                var vehiclesData = JsonConvert.DeserializeObject<VehicleResult>(vehcileResponse);



                // Return results with pagination data
                var result = vehiclesData.Results.Select(r => new
                {
                    makeID = r.Make_ID,
                    makeName = r.Make_Name,
                    model = r.Model_Name
                });
                return Json(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }




        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
