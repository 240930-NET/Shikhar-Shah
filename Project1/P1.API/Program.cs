using Microsoft.EntityFrameworkCore;
using P1.API.Data;
using P1.API.Model;
using P1.API.Repository;
using P1.API.Service;
using P1.API.Service.Interface;
using P1.API.Repository.Interface;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration["ConnectionStrings:ShahP1"];
builder.Services.AddDbContext<ShahP1Context>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ShahP1")));

builder.Services.AddScoped<IPetService, PetService>();

builder.Services.AddScoped<IPetRepository, PetRepository>();

builder.Services.AddControllers();

builder.Services.AddDbContext<ShahP1Context>(options => options.UseSqlServer(connectionString));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();