using SteveToRestaurants.Core;
using System;
using System.Collections.Generic;
using System.Text;

using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.IO;

namespace SteveToRestaurants.Data
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetRestaurantByName(string name);
        Restaurant GetById(int id);
        Restaurant Add(Restaurant newRestaurant);
        Restaurant Update(Restaurant updatedRestaurant);
        Restaurant Delete(int id);
        int Commit();
    }

    public class InMemoryRestaurantData : IRestaurantData
    {
        readonly List<Restaurant> restaurants;

        public InMemoryRestaurantData()
        {
            restaurants = new List<Restaurant>()
            {
                new Restaurant { Id = 1, Name = "Steve Pizza", Location = "Calgary", Cuisine = CuisineType.Italian },
                new Restaurant { Id = 2, Name = "Cinnamon Club", Location = "Edmonton", Cuisine = CuisineType.Italian },
                new Restaurant { Id = 3, Name = "La Costa", Location = "Calgary", Cuisine = CuisineType.Mexican },
                new Restaurant { Id = 4, Name = "Jerusalem Shawarma", Location = "Calgary", Cuisine = CuisineType.Indian },
                //new Restaurant { Id = 5, Name = "Pho Hoan Pasteur", Location = "Calgary", Cuisine = CuisineType.Vietnamienne },
                new Restaurant { Id = 6, Name = "Paros Real Greek", Location = "Calgary", Cuisine = CuisineType.Italian }

            };
        }

        public Restaurant GetById(int id)
        {
            return restaurants.SingleOrDefault(r => r.Id == id);
        }

        public Restaurant Add(Restaurant newRestaurant)
        {
            restaurants.Add(newRestaurant);
            newRestaurant.Id = restaurants.Max(r => r.Id) + 1;
            return newRestaurant;

        }
        public Restaurant Update(Restaurant updateRestaurant)
        {
            var restaurant = restaurants.SingleOrDefault(r => r.Id == updateRestaurant.Id);
            if (restaurant != null)
            {
                restaurant.Name = updateRestaurant.Name;
                restaurant.Location = updateRestaurant.Location;
                restaurant.Cuisine = updateRestaurant.Cuisine;
            }
            return restaurant;
        }
        public int Commit()
        {
            return 0;
        }
        public Restaurant Delete(int id)
        {
            var restaurant = restaurants.FirstOrDefault(r => r.Id == id);
            if (restaurant != null)
            {
                restaurants.Remove(restaurant);
            }
            return restaurant;
        }
        public IEnumerable<Restaurant> GetRestaurantByName(string name = null)
        {
            return from r in restaurants
                   where string.IsNullOrEmpty(name) || r.Name.StartsWith(name)
                   orderby r.Name
                   select r;

        }
    }
}
