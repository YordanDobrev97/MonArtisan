namespace MonArtisan.Services
{
    using Newtonsoft.Json;

    public class LocationObject
    {
        [JsonProperty("location")]
        public Location[] Location { get; set; }
    }
}
