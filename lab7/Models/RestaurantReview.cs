using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Lab7.Models
{
    public class RestaurantReview
    {
        [JsonPropertyName("restaurants")]
        public List<Restaurant> Restaurants { get; set; }
    }

    public class Restaurant
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [JsonPropertyName("foodType")]
        [Display(Name = "FoodType")]
        public string FoodType { get; set; }

        [JsonPropertyName("rating")]
        [Display(Name = "Rating (Out of 5)")]
        public int Rating { get; set; }

        [JsonPropertyName("cost")]
        [Display(Name = "Cost (Out of 5)")]
        public int Cost { get; set; }

        [JsonPropertyName("address")]
        public Address Address { get; set; }

        [JsonPropertyName("summary")]
        public string Summary { get; set; }
    }

    public class Address
    {
        [JsonPropertyName("city")]
        [Display(Name = "City")]
        public string City { get; set; }

        [JsonPropertyName("provinceState")]
        [Display(Name = "Province")]
        public string ProvinceState { get; set; }

        [JsonPropertyName("street")]
        [Display(Name = "Street Address")]
        public string Street { get; set; }

        [JsonPropertyName("postalZipCode")]
        [Display(Name = "Postal/Zip Code")]
        public string PostalZipCode { get; set; }
    }
}
