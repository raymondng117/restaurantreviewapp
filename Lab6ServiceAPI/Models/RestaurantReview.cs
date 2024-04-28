using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Lab6ServiceAPI.Models
{
    [XmlRoot("restaurant_reviews", Namespace = "http://www.algonquincollege.com/cst8259/labs")]
    public class RestaurantReview
    {
        [XmlElement("restaurant")]
        public List<Restaurant>? Restaurants { get; set; }

    }

    public class Restaurant
    {
        [XmlElement("id")]
        public int Id { get; set; }

        [XmlElement("name")]
        public string? Name { get; set; }

        [XmlElement("food_type")]
        public string? FoodType { get; set; }

        [XmlElement("rating")]
        [Display(Name = "Rating (best=5)")]
        public decimal? Rating { get; set; }

        [XmlElement("summary")]
        public string? Summary { get; set; }

        [XmlElement("cost")]
        [Display(Name = "Cost (most expensive=5)")]
        public decimal? Cost { get; set; }

        [XmlElement("address")]
        public Address? Address { get; set; }


    }

    public class Address
    {
        [XmlElement("city")]
        public string? City { get; set; }

        [XmlElement("state_province")]
        public string ProvinceState { get; set; }

        [XmlElement("street_address")]
        public string? Street { get; set; }

        [XmlElement("zip_postal_code")]
        public string? PostalZipCode { get; set; }
    }


}
