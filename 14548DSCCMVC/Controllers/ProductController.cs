using _14548DSCCMVC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
//using Microsoft.AspNetCore.Mvc;



namespace _14548DSCCMVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly HttpClient _client;
        private readonly string _apiBaseUrl = "http://localhost:5095/api/Product";

        public ProductController()
        {
            _client = new HttpClient();
        }

        // GET: Product
        public async Task<IActionResult> Index()
        {
            var response = await _client.GetAsync(_apiBaseUrl);
            if (response.IsSuccessStatusCode)
            {
                var products = await response.Content.ReadAsAsync<IEnumerable<Product>>();
                return View(products);
            }
            return View("Error");
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var response = await _client.GetAsync($"{_apiBaseUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                var product = await response.Content.ReadAsAsync<Product>();
                return View(product);
            }
            return View("Error");
        }

        // GET: Product/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                //var json = await JsonSerializer.Serialize(product);
                var json = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _client.PostAsync(_apiBaseUrl, content);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _client.GetAsync($"{_apiBaseUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                var product = await response.Content.ReadAsAsync<Product>();
                return View(product);
            }
            return View("Error");
        }

        // POST: Product/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (ModelState.IsValid && id == product.Id)
            {
                var json = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _client.PutAsync($"{_apiBaseUrl}/{id}", content);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _client.GetAsync($"{_apiBaseUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                var product = await response.Content.ReadAsAsync<Product>();
                return View(product);
            }
            return View("Error");
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _client.DeleteAsync($"{_apiBaseUrl}/{id}");
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));
            return View("Error");
        }
    }
}