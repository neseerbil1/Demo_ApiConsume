using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Demo_ApiConsume.Models;
using Newtonsoft.Json;

namespace Demo_ApiConsume.Controllers
{
    public class ExchangeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://booking-com.p.rapidapi.com/v1/metadata/exchange-rates?currency=TRY&locale=tl"),
                Headers =
    {
        { "X-RapidAPI-Key", "d495032cabmsh805945f067c2162p18a230jsn4bb2c50d7540" },
        { "X-RapidAPI-Host", "booking-com.p.rapidapi.com" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var models=JsonConvert.DeserializeObject<ExchangeViewListModel>(body);
                return View(models.exchange_rates);
            }
            //nested json data: içiçe json verisi
        }
        }
    }
