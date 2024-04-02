using FindANameServer.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FindANameServer;

public static class Endpoints
{
    public static void AddNameEndpoints(this WebApplication app)
    {
        app.MapGet("/names", async (UserManager<User> userManager, ClaimsPrincipal principal, INamesRepository namesRepository) =>
        {
            var userName = principal.Identity?.Name ?? "";

            var user = await userManager.FindByNameAsync(userName);

            if (user == null) return Results.Unauthorized();

            var names = namesRepository.GetRandom(user, 5);

            return Results.Ok(names);
        })
        .WithName("GetNames")
        .WithOpenApi()
        .RequireAuthorization();

        app.MapPost("/names", (INamesRepository namesRepository, [FromBody] string[] newNames) => {
            namesRepository.Add(newNames);

            return Results.Ok();
        })
        .WithName("PostNames")
        .WithOpenApi()
        .RequireAuthorization();
    }   
}
