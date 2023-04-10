using NeoVisualizer.Viewmodels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoVisualizer.NASA_API
{
    internal class OfflineRepository
    {
        private static readonly List<string> ImagePaths = new()
        {
            "/Resources/Images/asteroid0.jpg",
            "/Resources/Images/asteroid1.jpg",
            "/Resources/Images/asteroid2.jpeg",
        };
        public static async Task<List<NEODetail>> GetNEOsAsync(int pageNumber)
        {
            var list = new List<NEODetail>();

            pageNumber = Math.Clamp(pageNumber, 0, 2); //I only included 3 pages in the offline repo so I'm clamping the value
            using (StreamReader r = new StreamReader("../../../Resources/DataSource/0" + pageNumber.ToString() + ".json"))
            {
                string json = await r.ReadToEndAsync();
                NasaBrowseResponse? resp = JsonConvert.DeserializeObject<NasaBrowseResponse>(json);


                if (resp == null) return list;

                foreach (var neo in resp.near_earth_objects)
                {
                    var rndImgIdx = (int)neo.absolute_magnitude_h % ImagePaths.Count; //API doesnt give images so have to select one at random
                    list.Add(new NEODetail()
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
                        ImagePath = ImagePaths[rndImgIdx],
                    });
                }
            }

            return list;
        }
    }
}
