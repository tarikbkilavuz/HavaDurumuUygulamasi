using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Spectre.Console;

namespace HavaDurumuUygulamasi
{
    public class Program
    {
        public static async Task Main()
        {
            string startupTime = "Tarih: " + DateTime.Now;
            Console.WriteLine(startupTime);

            WeatherData istanbulWeather = await GetWeatherData("https://goweather.herokuapp.com/weather/istanbul");
            WeatherData ankaraWeather = await GetWeatherData("https://goweather.herokuapp.com/weather/ankara");
            WeatherData izmirWeather = await GetWeatherData("https://goweather.herokuapp.com/weather/izmir");

            AnsiConsole.MarkupLine("\n\n[underline]İstanbul Hava Durumu[/]\n");
            PrintWeather(istanbulWeather);
            AnsiConsole.MarkupLine("\n\n[underline]Ankara Hava Durumu[/]\n");
            PrintWeather(ankaraWeather);
            AnsiConsole.MarkupLine("\n\n[underline]İzmir Hava Durumu[/]\n");
            PrintWeather(izmirWeather);
        }

        public static async Task<WeatherData> GetWeatherData(string apiUrl)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    WeatherData weatherData = JsonConvert.DeserializeObject<WeatherData>(responseData);
                    return weatherData;
                }
                else
                {
                    Console.WriteLine("Bazı hava durumu bilgileri görüntülenemedi!\n");
                    return null;
                }
            }
        }

        public static void PrintWeather(WeatherData weather)
        {
            if (weather != null)
            {
                Console.WriteLine($"Sıcaklık: {weather.Temperature}");
                Console.WriteLine($"Rüzgar: {weather.Wind}");
                Console.WriteLine($"Durum: {weather.Description}\n");

                DateTime anlikTarih = DateTime.Now;

                foreach (var forecast in weather.Forecast)
                {
                    anlikTarih = anlikTarih.AddDays(1);

                    Console.WriteLine($"{anlikTarih.ToString("dddd", new System.Globalization.CultureInfo("tr-TR"))}: Sıcaklık {forecast.Temperature}, Rüzgar {forecast.Wind} ");
                }
            }
        }
    }
}
