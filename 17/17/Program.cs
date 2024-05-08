using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

class ChuckNorrisFactsApp
{
    private static readonly HttpClient client = new HttpClient();
    private const string chuckNorrisApiUrl = "https://api.chucknorris.io/jokes/random";

    static async Task Main(string[] args)
    {
        Console.WriteLine("Chuck Norris Facts App");

        while (true)
        {
            await DisplayRandomChuckNorrisFact();
            Thread.Sleep(120000); // Очікувати 2 хвилини перед оновленням факту
        }
    }

    static async Task DisplayRandomChuckNorrisFact()
    {
        try
        {
            HttpResponseMessage response = await client.GetAsync(chuckNorrisApiUrl);
            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();

            // Розпакувати відповідь API у вигляді об'єкта
            ChuckNorrisFact fact = JsonSerializer.Deserialize<ChuckNorrisFact>(responseBody);

            Console.WriteLine($"[{DateTime.Now:t}] {fact.Value}");
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"Помилка при запиті до API Chuck Norris: {e.Message}");
        }
        catch (JsonException e)
        {
            Console.WriteLine($"Помилка розпаковки відповіді: {e.Message}");
        }
    }
}

public class ChuckNorrisFact
{
    public string Value { get; set; }
}
