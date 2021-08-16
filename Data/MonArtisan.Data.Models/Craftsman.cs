namespace MonArtisan.Data.Models
{
    public class Craftsman : BaseUser
    {
        public string CompanyName { get; set; }

        public string CompanyNumber { get; set; }

        public string Address { get; set; }

        public decimal Longitude { get; set; }

        public decimal Latitude { get; set; }

        public string Profession { get; set; }

        public int Radius { get; set; }

        public string KbisDocument { get; set; }
    }
}