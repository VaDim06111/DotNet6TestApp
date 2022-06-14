using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using TestApp.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File($"logs\\log.txt", rollingInterval: RollingInterval.Day,
    outputTemplate: "{Timestamp:yyyy-MM-dd hh:mm:ss zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
    .CreateLogger();

// Add services to the container.
builder.Services.AddControllers(options =>
    {
        options.Filters.Add(typeof(LastVisitFilter));
    })
    .AddFluentValidation(options =>
    {
        options.ImplicitlyValidateChildProperties = true;
        options.ImplicitlyValidateRootCollectionElements = true;
        options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    });

// Add db connection
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Add MediatR
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<IMediator, Mediator>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "TestApp",
            Version = "v1"
        }
     );

    var filePath = Path.Combine(AppContext.BaseDirectory, "TestApp.xml");
    c.IncludeXmlComments(filePath);
});

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
        builder.SetIsOriginAllowed(_ => true)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.UseStatusCodePages();

app.MapControllers();

app.Run();
