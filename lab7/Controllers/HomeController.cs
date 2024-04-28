using Lab7.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Lab7.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        // private readonly string serivceUrl = "http://localhost:5151/restaurantreview";
        private readonly string serivceUrl = "http://api:8080/restaurantreview";

        private RestaurantReview GetRestaurantReviewFromService()
        {
            RestaurantReview restaurantReview = new RestaurantReview();
            HttpClient client = new HttpClient();
            using var response = client.GetAsync(serivceUrl).Result;
            if (response.IsSuccessStatusCode)
            {
                string apiResponse = response.Content.ReadAsStringAsync().Result;
                restaurantReview = JsonSerializer.Deserialize<RestaurantReview>(apiResponse);
            }

            return restaurantReview;
        }


        public IActionResult Index()
        {
            RestaurantReview restaurantReview = GetRestaurantReviewFromService();
            return View(restaurantReview);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            RestaurantReview restaurantReview = GetRestaurantReviewFromService();
            Restaurant restaurant = restaurantReview.Restaurants.Find(r => r.Id == id);
            return View(restaurant);
        }


        public IActionResult Create()
        {
            Restaurant restaurant = new Restaurant();
            return View(restaurant);
        }

        [HttpPost]

        public IActionResult Create(Restaurant newRestaurant)
        {
            string newRestaurantJson = JsonSerializer.Serialize(newRestaurant);
            var content = new StringContent(newRestaurantJson, Encoding.UTF8, "application/json");

            HttpClient client = new HttpClient();
            using var response = client.PostAsync(serivceUrl, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));

            }
            else
            {
                return View(newRestaurant);
            }
        }

        [HttpPost]
        public IActionResult Edit(Restaurant updatedRestaurant)
        {
            string updatedRestaurantJson = JsonSerializer.Serialize(updatedRestaurant);
            var content = new StringContent(updatedRestaurantJson, Encoding.UTF8, "application/json");

            HttpClient client = new HttpClient();
            using var response = client.PutAsync(serivceUrl, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));

            } else
            {
                return View(updatedRestaurant);
            }

        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            HttpClient client = new HttpClient();
            string deleteUrl = $"{serivceUrl}/{id}";
            using var response = client.DeleteAsync(deleteUrl).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
