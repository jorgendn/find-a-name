using FindANameServer.Domain;
using FindANameServer.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FindANameServer;

public static class Endpoints
{
    public static void AddNameEndpoints(this WebApplication app)
    {
        app.MapGet("/names", async (UserManager<User> userManager, ClaimsPrincipal principal, INamesRepository namesRepository, [FromQuery] int limit = 5) =>
        {
            var userName = principal.Identity?.Name ?? "";

            var user = await userManager.FindByNameAsync(userName);

            if (user == null) return Results.Unauthorized();

            var names = await namesRepository.Get(user, limit);

            return Results.Ok(names);
        })
        .WithName("GetNames")
        .WithOpenApi()
        .RequireAuthorization();

        app.MapPost("/names", async (INamesRepository namesRepository, [FromBody] string[] newNames) => {
            await namesRepository.Add(newNames);

            return Results.Ok();
        })
        .WithName("PostNames")
        .WithOpenApi()
        .RequireAuthorization();

        app.MapPost("/rejectNames", async (UserManager<User> userManager, ClaimsPrincipal principal, INamesRepository namesRepository, [FromBody] int[] rejected) =>
        {
            var userName = principal.Identity?.Name ?? "";

            var user = await userManager.FindByNameAsync(userName);

            if (user == null) return Results.Unauthorized();

            await namesRepository.Reject(user, rejected);

            return Results.Ok();
        })
        .WithName("RejectNames")
        .WithOpenApi()
        .RequireAuthorization();
    }   
}
