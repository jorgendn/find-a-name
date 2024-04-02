namespace FindANameServer;

public static class Endpoints
{
    public static void AddNameEndpoints(this WebApplication app)
    {
        app.MapGet("/names", () =>
        {
            List<string> names = ["Jakob", "Jens", "Kolera"];

            return names;
        })
        .WithName("GetNames")
        .WithOpenApi()
        .RequireAuthorization();
    }
}
