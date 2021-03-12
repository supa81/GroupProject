using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PawMates.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PawMates.Services
{
    public class DistanceMatrixService
    {
        public DistanceMatrixService()
        {

        }
        public string GetDistanceMatrixURL(Owner originOwner, Dog destinationDog)
        {
            return $"https://maps.googleapis.com/maps/api/distancematrix/json?units=imperial&origins={originOwner.OwnerLatitude},{originOwner.OwnerLongitude}&destinations={destinationDog.OwnerLat}%2C{destinationDog.OwnerLng}&key=" + APIKeys.GOOGLE_API_KEY;

        }

        public async Task<double> GetDistanceInMeters(Owner originOwner, Dog destinationDog)
        {
            string apiURL = GetDistanceMatrixURL(originOwner, destinationDog);
            double distanceInMiles = 0;
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
                    JToken rows = jsonResults["rows"][0];
                    JToken elements = rows["elements"][0];
                    JToken distance = elements["distance"];

                    var distanceInMeters = (int)distance["value"];

                    distanceInMiles = MeterConverter.ConvertMetersToMiles(distanceInMeters);
                }
            }
            return distanceInMiles;
        }


    }
}
