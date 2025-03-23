namespace CodeVersioning.Models.V2
{
    public class WeatherForecast
    {
        public DateOnly Date { get; set; }
        public string? Summary { get; set; }

        public Guid YourApiKey { get; set; }
    }
}
