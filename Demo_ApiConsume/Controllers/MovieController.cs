using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Demo_ApiConsume.Models;
using Newtonsoft.Json;
using static System.Net.WebRequestMethods;

namespace Demo_ApiConsume.Controllers
{
    public class MovieController : Controller
    {
        public async Task<IActionResult> Index()
        {
            // veritabanından veya başka bir kaynaktan okunan verileri saklamak ve işlemek için kullanılabilir.
            List<MovieListViewModel> movies = new List<MovieListViewModel>();
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://imdb-top-100-movies.p.rapidapi.com/"),
                Headers =
    {
        { "X-RapidAPI-Key", "d495032cabmsh805945f067c2162p18a230jsn4bb2c50d7540" },
        { "X-RapidAPI-Host", "imdb-top-100-movies.p.rapidapi.com" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                //Bu kod bir HTTP isteği yapar ve döndürülen cevabın durum kodunu
                //    kontrol eder.Eğer durum kodu
                //    bir başarı kodu değilse(örneğin 404, 500 gibi),
                //    EnsureSuccessStatusCode bir HttpRequestException fırlatır
                response.EnsureSuccessStatusCode();
                //body metodu, bir HTTP isteğinin içeriğini temsil eden bir HttpContent nesnesini
                //    döndürür.Bu içerik, isteğin gövdesinde gönderilen verileri içerebilir. Örneğin, 
                //    bir POST isteğinde gönderilen form verileri veya bir PUT isteğinde gönderilen JSON 
                //    verileri gibi. Bu veriler, HttpContent nesnesinde saklanır ve daha sonra kullanılmak 
                //    üzere okunabilir veya değiştirilebilir.
                var body = await response.Content.ReadAsStringAsync();
                //Bu kod parçacığı, verilen bir JSON dizesini işlemek için kullanılır. Özellikle,
                //JsonConvert.DeserializeObject<T>() yöntemi, verilen JSON dizesini belirtilen bir veri türüne
                //dönüştürür. Bu örnekte, List<MovieListViewModel> veri türü kullanılmıştır, bu nedenle döndürülen
                //sonuç bir List<MovieListViewModel> nesnesi olacaktır. Döndürülen nesne movies değişkenine atanır
                movies = JsonConvert.DeserializeObject<List<MovieListViewModel>>(body);               
                return View(movies);


            }
          
        }
    }
}
