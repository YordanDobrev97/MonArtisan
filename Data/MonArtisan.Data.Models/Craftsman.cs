namespace MonArtisan.Data.Models
{
    public class Craftsman : BaseUser
    {
        public string CompanyName { get; set; }

        public string CompanyNumber { get; set; }

        public string Address { get; set; }

        public string ZipCode { get; set; }

        public string Profession { get; set; }

        public int Radius { get; set; }

        public string KbisDocument { get; set; }
    }
}