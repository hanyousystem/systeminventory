using systeminventory_sample.Models.DbFirst;
using Microsoft.EntityFrameworkCore;

//このクラス自体がEntrypointの役割を担っている
var builder = WebApplication.CreateBuilder(args);

//内製のデータを作成
// Add database context to DI container using SQLite database.
builder.Services.AddDbContext<inHouseDbContext>(options =>
{
    var path = builder.Configuration.GetConnectionString("inHouseDbConnection");
    options.UseSqlite(path);

   // var path = builder.Configuration.GetConnectionString("SQLConnection");
   // options.UseSqlServer(path);
});

// Add controllers to the DI container.
builder.Services.AddControllers();

// Add Swagger/OpenAPI services to the DI container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Use Swagger and SwaggerUI for testing and development.
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Serve static files from wwwroot folder.
app.UseStaticFiles();

// Enable routing middleware to match incoming requests to an endpoint.
app.UseRouting();

// Redirect all HTTP requests to HTTPS.
app.UseHttpsRedirection();

// Allow CORS for all methods, headers and origins.
app.UseCors(x => x.AllowAnyMethod()
            .AllowAnyHeader()
            .AllowAnyOrigin());

// Map the endpoints to the controllers and start listening.
app.UseEndpoints(x => x.MapControllers());
app.Run();
