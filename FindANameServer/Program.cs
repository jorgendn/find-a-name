using FindANameServer;
using FindANameServer.Domain;
using FindANameServer.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<NameDbContext>(options =>
    options.UseSqlServer("Data Source=LOCALHOST;Initial Catalog=dbNames;Integrated Security=False;User ID=dbUserNames;Password=P4ssw0rd1;MultipleActiveResultSets=True;TrustServerCertificate=True")
);

builder.Services.AddCors(options => options.AddPolicy("AllowAnything", builder =>
{
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
}));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<User>().AddEntityFrameworkStores<NameDbContext>();

builder.Services.AddScoped<INamesRepository, NamesRepository>();

var app = builder.Build();

app.UseCors("AllowAnything");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapIdentityApi<User>();

app.UseHttpsRedirection();

app.AddNameEndpoints();

app.MapPost("/logout", async (SignInManager<User> signInManager, [FromBody] object empty) =>
{
    if (empty != null)
    {
        await signInManager.SignOutAsync();
        return Results.Ok();
    }
    return Results.Unauthorized();
})
.WithOpenApi()
.RequireAuthorization();

app.Run();
