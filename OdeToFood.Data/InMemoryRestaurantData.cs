using System;
using System.Collections.Generic;
using System.Linq;
using OdeToFood.Core;

namespace OdeToFood.Data
{
    public class InMemoryRestaurantData : IRestaurantData
    {
        List<Restaurant> restaurants;

        public InMemoryRestaurantData()
        {
            restaurants = new List<Restaurant>
            {
                new Restaurant{ Id = 1, Name="Scott's Pizza", Location= "Marylebone Station", Cuisine=CuisineType.Italian },
                new Restaurant{ Id = 2, Name="Cinnamon Club", Location= "Mondon", Cuisine=CuisineType.Indian },
                new Restaurant{ Id = 3, Name="La Costa", Location= "Califonia", Cuisine=CuisineType.Mexican },
            };
        }

        public Restaurant Add(Restaurant newRestaurant)
        {
            restaurants.Add(newRestaurant);
            newRestaurant.Id = restaurants.Max(r => r.Id) + 1;
            return newRestaurant;
        }

        public int Commit()
        {
            return restaurants.Count();
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

        public Restaurant GetById(int id)
        {
            return restaurants.SingleOrDefault(r => r.Id == id);
        }

        public int GetCountOfRestaurants()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Restaurant> GetRestaurantsByName()
        {
            return GetRestaurantsByName(null);
        }

        public IEnumerable<Restaurant> GetRestaurantsByName(string name)
        {
            return restaurants.Where(r => string.IsNullOrEmpty(name) || 
                                          r.Name.StartsWith(name,StringComparison.InvariantCultureIgnoreCase))
                                         .OrderBy(x => x.Name);
        }

        public Restaurant Update(Restaurant updatedRestaurant)
        {
            var restaurant = restaurants.SingleOrDefault(r => r.Id == updatedRestaurant.Id);
            if (restaurant !=null)
            {
                restaurant.Name = updatedRestaurant.Name;
                restaurant.Location = updatedRestaurant.Location;
                restaurant.Cuisine = updatedRestaurant.Cuisine;
            }
            return restaurant;
        }
    }
}
