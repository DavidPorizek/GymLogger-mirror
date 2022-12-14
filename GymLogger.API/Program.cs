using Microsoft.EntityFrameworkCore;
using GymLogger.Context;
using System.Reflection;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    var builtInFactory = options.InvalidModelStateResponseFactory;

    options.InvalidModelStateResponseFactory = context =>
    {
        var logger = context.HttpContext.RequestServices.GetService<ILoggerFactory>();

        return builtInFactory(context);
    };
});

builder.Services.AddDbContext<GymLoggerContext>(opt =>
    // opt.UseSqlServer(builder.Configuration.GetConnectionString("GymLoggerContext")));
    opt.UseInMemoryDatabase("GymLoggerContext"));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
