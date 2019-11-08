using System;
using System.Net.Http;
using System.Threading.Tasks;
using Coravel.Invocable;
using Newtonsoft.Json.Linq;

public class Qod : IInvocable
{
    public async Task Invoke()
    {
        var httpClient = new HttpClient();
        var quoteJson = JObject.Parse(await httpClient.GetStringAsync("http://quotes.rest/qod.json"));
        string quote = quoteJson["contents"]["quotes"][0].ToString();

        Console.WriteLine(quote);

        //return Task.CompletedTask;
    }
}