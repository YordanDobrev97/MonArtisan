namespace MonArtisan.Services
{
    using System.Net.Http;
    using System.Threading.Tasks;

    using Newtonsoft.Json;

    public class ZipToLocation
    {
        public static async Task<Location> ConvertZipCodeToLocation(string zipCode, string countryCode)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"https://thezipcodes.com/api/v1/search?zipCode={zipCode}&countryCode={countryCode}&apiKey=27c2d111c10ae9cd459ec5b53de96c31");
            string responseBody = await response.Content.ReadAsStringAsync();
            var json = JsonConvert.DeserializeObject<LocationObject>(responseBody).Location[0];
            var result = new Location { Latitude = json.Latitude, Longitude = json.Longitude };

            return result;
        }
    }
}
