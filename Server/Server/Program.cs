using Microsoft.EntityFrameworkCore;
using Server.Persistance.Context;
using Server.Persistance.Repositories.Candidates;
using Server.Persistance.Repositories.CandidateSkills;
using Server.Persistance.Repositories.Skills;
using Server.Services.CandidateService;
using Server.Services.SkillService;

var builder = WebApplication.CreateBuilder(args);

// Repositories
builder.Services.AddScoped<ISkillRepository, SkillRepository>();
builder.Services.AddScoped<ICandidateSkillRepository, CandidateSkillRepository>();
builder.Services.AddScoped<ICandidateRepository, CandidateRepository>();

// Database Context
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Services
builder.Services.AddScoped<ISkillService, SkillService>();
builder.Services.AddScoped<ICandidateService, CandidateService>();

// Controllers
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
