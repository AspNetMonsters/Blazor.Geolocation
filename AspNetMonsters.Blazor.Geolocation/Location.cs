namespace AspNetMonsters.Blazor.Geolocation
{
    public class Location
    {
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public decimal Accuracy { get; set; }

        public override string ToString()
        {
            return $"Location: ({Latitude}, {Longitude}) with accuracy {Accuracy}";
        }
    }
}
