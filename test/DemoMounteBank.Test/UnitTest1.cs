using System.Text.Json;
using FluentAssertions;

namespace DemoMounteBank.Test;

public class UnitTest1
{
    private record ResponseObject
    {
        public string id {get;set;}
    }

    [Fact]
    public async Task Test1Async()
    {
        var client = new HttpClient();

        // Set the base address of the mountebank server
        client.BaseAddress = new Uri("https://628706e47864d2883e7d2b04.mockapi.io");

        // Make a GET request to a mock endpoint
        HttpResponseMessage response = await client.GetAsync("/api/v1/users/1");
        response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();
        // Deserialize the response content into a JObject
        var json = JsonSerializer.Deserialize<ResponseObject>(responseBody);
        json.id.Should().Be("1");
    }
}
