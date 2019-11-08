using System;
using System.Net.Http;
using System.Threading.Tasks;
using Coravel.Invocable;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QuoteOfTheDay;

public class QotdTask : IInvocable
{
    public async Task Invoke()
    {
        var httpClient = new HttpClient();
        var quoteJson = JObject.Parse(await httpClient.GetStringAsync("http://quotes.rest/qod.json"));
        var quote = JsonConvert.DeserializeObject<Qotd>(quoteJson["contents"]["quotes"][0].ToString());

        Console.WriteLine($"Quote from {quote.Author}: {quote.Quote}");

        //return Task.CompletedTask;
    }
}