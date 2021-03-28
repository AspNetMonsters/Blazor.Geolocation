namespace AspNetMonsters.Blazor.Geolocation
{
    public class Location
    {
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public decimal? Altitude { get; set; }
        public decimal Accuracy { get; set; }
        public decimal? AltitudeAccuracy { get; set; }
        public decimal? Heading { get; set; }
        public decimal? Speed { get; set; }


        public override string ToString()
        {
            return $"Location: ({Latitude}, {Longitude}) with accuracy {Accuracy}";
        }
    }
}
