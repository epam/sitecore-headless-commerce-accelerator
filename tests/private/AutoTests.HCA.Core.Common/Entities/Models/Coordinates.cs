namespace AutoTests.HCA.Core.Common.Entities.Models
{
    public class Coordinates
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public Coordinates()
        {
        }

        public Coordinates(double lat, double lon)
        {
            Latitude = lat;
            Longitude = lon;
        }
    }
}
