using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Lab6ServiceAPI.Models;
using System.Collections;


namespace Lab6ServiceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RestaurantReviewController : ControllerBase
    {

        // method to get all restaurant reviews xml
        static private RestaurantReview GetRestaurantReviewsFromXml()
        {
            string xmlFilePath = Path.GetFullPath("Data/Restaurant_review.xml");

            using (FileStream xs = new FileStream(xmlFilePath, FileMode.Open))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(RestaurantReview));
                RestaurantReview restaurantReviews = (RestaurantReview)serializer.Deserialize(xs);

                return restaurantReviews;
            }
        }

        // method to save all restaurant reviews xml
        private void WritePersonsToXml(RestaurantReview restaurantReview)
        {
            string xmlFilePath = Path.GetFullPath("Data/Restaurant_review.xml");
            using (FileStream xs = new FileStream(xmlFilePath, FileMode.Create))
            {
                XmlSerializer serializor = new XmlSerializer(typeof(RestaurantReview));
                serializor.Serialize(xs, restaurantReview);
            }
        }

        RestaurantReview restaurantReviews = GetRestaurantReviewsFromXml();

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(restaurantReviews);
        }


        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {

            Restaurant? restaurant = restaurantReviews.Restaurants.FirstOrDefault(r => r.Id == id);

            if (restaurant == null)
            {
                return NotFound($"id {id} does not exist");
            }
            return Ok(restaurant);
        }


        // Get all restaurant names
        [Route("[action]")]
        [HttpGet]
        public IActionResult GetAllRestaurantNames()
        {
            List<string> names = new List<string>();

            foreach (Restaurant r in restaurantReviews.Restaurants)
            {
                names.Add(r.Name);
            }
            
            return Ok(names);
        }

        [HttpPost]
        public IActionResult Post(Restaurant newRestaurant)
        {
            int newId;

            List<Restaurant> restaurants = restaurantReviews.Restaurants;

            newId = restaurantReviews.Restaurants.Last().Id + 1;

            newRestaurant.Id = newId;
            restaurantReviews.Restaurants.Add(newRestaurant);
           
            WritePersonsToXml(restaurantReviews);

            // display the newly-added restaurant
            return CreatedAtAction(nameof(Get), new { id = newRestaurant.Id }, newRestaurant);
        }

        // Update a restaurant review by id
        [HttpPut("{id}")]
        public IActionResult Update(int id,  Restaurant updatedRestaurant)
        {
            List<Restaurant> restaurants = restaurantReviews.Restaurants;

            for (int i = 0; i < restaurants.Count; i++)
            {
                if (restaurants[i].Id == id)
                {
                    restaurants[i] = updatedRestaurant;
                    break;
                }
            }

            WritePersonsToXml(restaurantReviews);
            return CreatedAtAction(nameof(Get), new { id = updatedRestaurant.Id }, updatedRestaurant);

        }

        // Update a restaurant review without an id
        [HttpPut]
        public IActionResult Update( [FromBody] Restaurant restaurantToUpdate)
        {
            List<Restaurant> restaurants = restaurantReviews.Restaurants;

            for (int i = 0; i < restaurants.Count; i++)
            {
                if (restaurants[i].Id == restaurantToUpdate.Id)
                {
                    restaurants[i] = restaurantToUpdate;
                    break;
                }
            }

            WritePersonsToXml(restaurantReviews);
            return CreatedAtAction(nameof(Get), new { id = restaurantToUpdate.Id }, restaurantToUpdate);
        }

        // Delete a restaurant by an id
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            List<Restaurant> restaurants = restaurantReviews.Restaurants;

            for (int i = 0; i < restaurants.Count; i++)
            {
                if (restaurants[i].Id == id)
                {
                    restaurants.Remove(restaurants[i]);
                    break;
                }
            }

            WritePersonsToXml(restaurantReviews);
            return NoContent();
        }
    }
}
