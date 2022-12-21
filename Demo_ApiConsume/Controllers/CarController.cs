using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Demo_ApiConsume.Models;
using Newtonsoft.Json;

namespace Demo_ApiConsume.Controllers
{
    public class CarController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<CarListViewModel> carLists = new List<CarListViewModel>();
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://car-data.p.rapidapi.com/cars?limit=10&page=0"),
                Headers =
    {
        { "X-RapidAPI-Key", "d495032cabmsh805945f067c2162p18a230jsn4bb2c50d7540" },
        { "X-RapidAPI-Host", "car-data.p.rapidapi.com" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();  
                carLists=JsonConvert.DeserializeObject<List<CarListViewModel>>(body);   
                return View(carLists);
            }
        }
    }
}
