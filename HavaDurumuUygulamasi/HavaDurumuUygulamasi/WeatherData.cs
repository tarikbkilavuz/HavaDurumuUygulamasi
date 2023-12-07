using System;

namespace HavaDurumuUygulamasi
{
    public class WeatherData
    {
        public string Description { get; set; }
        public string Temperature { get; set; }
        public string Wind { get; set; }
        public ForecastData[] Forecast { get; set; }
    }
}