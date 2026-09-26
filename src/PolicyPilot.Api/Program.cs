using Microsoft.EntityFrameworkCore;
using PolicyPilot.Api.Data;
using PolicyPilot.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PolicyPilotDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PolicyPilot")));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.MapGet("/", () => Results.Redirect("/swagger"));
app.MapPolicyEndpoints();

app.Run();
