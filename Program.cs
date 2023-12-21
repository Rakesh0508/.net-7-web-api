using Microsoft.EntityFrameworkCore;
using Serilog;
using WorldAPI.Common;
using WorldAPI.Data;
using WorldAPI.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region Configure CORS

builder.Services.AddCors(options =>
{
    options.AddPolicy("CustomPolicy", x=>x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

#endregion

builder.Services.AddDbContext<ApplicationDbContext>(options =>
   options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


#region Configure AutoMapper

builder.Services.AddAutoMapper(typeof(MappingProfile));

#endregion

#region

builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddTransient<ICountryRepository,CountryRepository>();
builder.Services.AddTransient<IStatesRepository, StatesRepository>();

#endregion 

#region Configure Serilog

builder.Host.UseSerilog((context, config) =>
{
    config.WriteTo.File("Logs/logs.txt", rollingInterval: RollingInterval.Day);
    
    if(context.HostingEnvironment.IsProduction() == false)
    {
        config.WriteTo.Console();
    }
});

#endregion


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CustomPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
