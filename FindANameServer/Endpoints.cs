using Microsoft.AspNetCore.Mvc;

namespace FindANameServer;

public static class Endpoints
{
    private static List<string> names = ["Jakob", "Jens", "Kolera"];

    public static void AddNameEndpoints(this WebApplication app)
    {
        app.MapGet("/names", () =>
        {
            return names;
        })
        .WithName("GetNames")
        .WithOpenApi();

        app.MapPost("/names", ([FromBody] string[] newNames) => {
            foreach (var name in newNames)
            {
                names.Add(name);
            }

            return Results.Ok();
        })
        .WithName("PostNames")
        .WithOpenApi();
    }   
}
