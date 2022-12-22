using Demo_ApiConsume.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Demo_ApiConsume.Controllers
{
    public class CategoryController : Controller
    {//istekleri yapmak için bağlantı
        private readonly IHttpClientFactory _httpClientFactory;

        public CategoryController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

      
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            //adrese verileri getirmek için istek attık
            var responseMessage = await client.GetAsync("http://localhost:55785/api/Category");
            //responseMessage adresin içine ulaşabiliyorsa
            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            { 
                var jsonData = await responseMessage.Content.ReadAsStringAsync();               
                //json yapısı nesne formatına dönüştürülüyor
                var results = JsonConvert.DeserializeObject<List<CategoryResponseModel>>(jsonData);
                return View(results);
            }
            return View();
        }
        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryResponseModel p)
        {
            var client = _httpClientFactory.CreateClient();            
            //nesne formatı json yapısına dönüştürülüyor
            var jsonData =JsonConvert.SerializeObject(p);
            
            StringContent content =new StringContent(jsonData,Encoding.UTF8,"application/json");
            //PostAsync metodu kullanıldığında HttpPost attribitune gerek kalmıyor
            var responseMessage = await client.PostAsync("http://localhost:55785/api/Category", content);
            if(responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            { 
            return View();
            }
        }
      
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var client=_httpClientFactory.CreateClient();   
            await client.DeleteAsync($"http://localhost:55785/api/Category/?id={id}");
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCategory(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"http://localhost:55785/api/Category/{id}");           
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<CategoryResponseModel>(jsonData);
                return View(data);

            }
            else
            {
                return View();
            }

        }
        [HttpPost]
        public async Task<IActionResult> UpdateCategory(CategoryResponseModel p)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(p);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("http://localhost:55785/api/Category", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");   
            }
            else
            {
                return View(); 
            } 
        }

    }
}
