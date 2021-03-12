using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PawMates.Models;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PawMates.Data;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PawMates.Services
{
    public class GeocodingService
    {
        public GeocodingService()
        {

        }

        public string GetGeocodingURL(Owner owner)
        {
            return $"https://maps.googleapis.com/maps/api/geocode/json?address={owner.State}+{owner.ZipCode}+&key=" + APIKeys.Google_API_KEY;
        }

        public async Task<Owner> GetGeocoding(Owner owner)
        {
            string apiURL = GetGeocodingURL(owner);

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(apiURL);
                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    JObject jsonResults = JsonConvert.DeserializeObject<JObject>(data);
                    JToken results = jsonResults["results"][0];
                    JToken location = results["geometry"]["location"];

                    owner.OwnerLatitude = (double)location["lat"];
                    owner.OwnerLongitude = (double)location["lng"];
                }
            }
            return owner;
        }
    }
}
