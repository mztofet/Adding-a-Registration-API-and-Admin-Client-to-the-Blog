using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

string apiKey = config["ApiSettings:ApiKey"]!;

HttpClient client = new();

client.BaseAddress = new Uri("http://localhost:5281/");

Console.WriteLine("1. Create Admin");
Console.WriteLine("2. Create Moderator");
Console.WriteLine("3. Create Author");

var choice = Console.ReadLine();

switch (choice)
{
    case "1":

        var admin = new
        {
            ApiKey = apiKey,
            Username = "consoleadmin@test.com",
            Password = "Password123!"
        };

        var adminResponse = await client.PostAsJsonAsync(
            "api/auth/registeradmin",
            admin);

        Console.WriteLine(
            await adminResponse.Content.ReadAsStringAsync());

        break;

    case "2":

        var moderator = new
        {
            ApiKey = apiKey,
            Username = "consolemoderator@test.com",
            Password = "Password123!"
        };

        var modResponse = await client.PostAsJsonAsync(
            "api/auth/registermoderator",
            moderator);

        Console.WriteLine(
            await modResponse.Content.ReadAsStringAsync());

        break;

    case "3":

        var author = new
        {
            ApiKey = apiKey,
            Username = "consoleauthor@test.com",
            Password = "Password123!",
            BlogTitle = "Console Blog",
            Subtitle = "Created from Console",
            Description = "Author created via BlogConsole"
        };

        var authorResponse = await client.PostAsJsonAsync(
            "api/auth/registerauthor",
            author);

        Console.WriteLine(
            await authorResponse.Content.ReadAsStringAsync());

        break;
}