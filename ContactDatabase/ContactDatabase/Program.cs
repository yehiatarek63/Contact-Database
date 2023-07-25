using ContactDatabase;
using EdgeDB;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddEdgeDB(EdgeDBConnection.FromInstanceName("Contact_Database"), config =>
{
    config.SchemaNamingStrategy = INamingStrategy.SnakeCaseNamingStrategy;
});
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();
builder.Services.AddScoped<IPasswordHasher<Contact>, PasswordHasher<Contact>>();
var app = builder.Build();


app.MapDelete("/remove-contact", async (HttpContext context, EdgeDBClient client) =>
{
    if(context.User.IsInRole("Admin"))
    {
        using var reader = new StreamReader(context.Request.Body);
        var id = await reader.ReadToEndAsync();
        Guid guidId = Guid.Parse(id);
        if (string.IsNullOrEmpty(id))
        {
            return Results.BadRequest();
        }
        await client.ExecuteAsync("DELETE Contact FILTER .id = <uuid>$id", new Dictionary<string, object?>
        {
            { "id", guidId }
        });
        return Results.Ok();
    }
    return Results.Unauthorized();
});
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
