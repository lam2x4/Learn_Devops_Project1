using API_Learn_Devops.Data;
using API_Learn_Devops.Middleware;
using API_Learn_Devops.Repositories;
using API_Learn_Devops.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositories & Services
//builder.Services.AddScoped<ITodoRepository, TodoRepository>();
builder.Services.AddScoped<ITodoService, TodoService>();
builder.Services.AddScoped<ITodoRepository, TodoRepository>();
// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy => policy.AllowAnyOrigin()  // <--- Thay ??i ? ?ây: Cho phép t?t c? các ngu?n
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();

// Seed Data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate(); // Automatically apply migrations
        DbInitializer.Initialize(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred creating the DB.");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var disableHttps = builder.Configuration.GetValue<bool>("DisableHttpsRedirection")
                    || string.Equals(Environment.GetEnvironmentVariable("DISABLE_HTTPS_REDIRECTION"), "true", StringComparison.OrdinalIgnoreCase);
if (!disableHttps)
{
    app.UseHttpsRedirection();
}

app.UseCors("AllowReactApp"); 
app.UseAuthorization();

app.MapControllers();

app.Run();
