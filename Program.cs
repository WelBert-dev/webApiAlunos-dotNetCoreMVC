using Microsoft.EntityFrameworkCore;
using webApiAlunos.Context;
using webApiAlunos.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add classe de context SqlServer

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Add Injeção de dependência do alunoService
builder.Services.AddScoped<IAlunoServices, AlunoServices>();


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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
