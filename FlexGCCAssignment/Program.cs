using DataLayer;
using Microsoft.EntityFrameworkCore;
using ServiceLayer;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddAutoMapper(cfg => cfg.AddMaps(typeof(Program).Assembly)
, typeof(MappingProfile));
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<WorkspaceDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IWorkRequestRepository, WorkRequestRepository>();

// Register Service Layer
builder.Services.AddScoped<IWorkRequestService, WorkRequestService>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();


app.MapControllers();

app.Run();
