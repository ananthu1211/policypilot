using Microsoft.EntityFrameworkCore;
using Microsoft.SemanticKernel;
using PolicyPilot.Api.Data;
using PolicyPilot.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PolicyPilotDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PolicyPilot")));

builder.Services.AddSingleton(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();

    var endpoint = configuration["AzureOpenAI:Endpoint"];
    var apiKey = configuration["AzureOpenAI:ApiKey"];
    var chatDeploymentName = configuration["AzureOpenAI:ChatDeploymentName"];

    if (string.IsNullOrWhiteSpace(endpoint) ||
        string.IsNullOrWhiteSpace(apiKey) ||
        string.IsNullOrWhiteSpace(chatDeploymentName))
    {
        throw new InvalidOperationException(
            "Azure OpenAI settings are missing. Set AzureOpenAI:Endpoint, AzureOpenAI:ApiKey, and AzureOpenAI:ChatDeploymentName (User Secrets locally).");
    }

    var kernelBuilder = Kernel.CreateBuilder();
    kernelBuilder.AddAzureOpenAIChatCompletion(
        deploymentName: chatDeploymentName,
        endpoint: endpoint,
        apiKey: apiKey);

    return kernelBuilder.Build();
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.MapGet("/", () => Results.Redirect("/swagger"));
app.MapControllers();
app.MapPolicyEndpoints();

app.Run();

public partial class Program;
