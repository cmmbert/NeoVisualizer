using NeoVisualizer.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace NeoVisualizer.NASA_API
{
    public class NasaNeoApi
    {
        private const string BaseUrl = "https://api.nasa.gov/neo/rest/v1/";
        private const string extendedUrl = "neo/browse";
        private const string apiKey = "?api_key=4SOS8QU6EbSYdtpvLOhczLz0y7R0fDavh8W6IOF3";

        public static async Task<List<NEOModel>> GetNEOsAsync(int pageNumber)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));


            var uri = extendedUrl + apiKey + "&page=" + pageNumber.ToString();

            var response = await client.GetAsync(uri);
            NasaBrowseResponse? resp = null;
            if (response.IsSuccessStatusCode)
            {
                resp = JsonConvert.DeserializeObject<NasaBrowseResponse>(await response.Content.ReadAsStringAsync());
            }
            

            var list = new List<NEOModel>();

            if (resp == null) return list;

            foreach (var neo in resp.near_earth_objects)
            {
                list.Add(new NEOModel()
                {
                    Id = neo.id,
                    NameLimited = neo.name_limited,
                    Name = neo.name,
                    FirstObserved = DateTime.Parse(neo.orbital_data.first_observation_date, new CultureInfo("en-US")),
                    LastObserved = DateTime.Parse(neo.orbital_data.last_observation_date, new CultureInfo("en-US")),
                    Magnitude = neo.absolute_magnitude_h,
                    Hazardous = neo.is_potentially_hazardous_asteroid,
                    DiameterMin = neo.estimated_diameter.kilometers.estimated_diameter_min * 10,
                    DiameterMax = neo.estimated_diameter.kilometers.estimated_diameter_max * 10,
                });
            }


            return list;
        }
    }
}
