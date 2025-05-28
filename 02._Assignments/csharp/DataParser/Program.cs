using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models; // For Swagger metadata
using Swashbuckle.AspNetCore.SwaggerUI; // For Swagger UI

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add Swagger generation
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My API",
        Version = "v1"
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // To resolve ambiguity, use the second overload of UseSwagger (no routeTemplate)
    app.UseSwagger();  // Uses the second overload (no string, no Action<SwaggerDocument>)

    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
        c.RoutePrefix = string.Empty;  // Makes Swagger UI accessible at the root
    });
}

app.UseHttpsRedirection();
app.MapControllers(); // Map controllers to routes

app.Run();
