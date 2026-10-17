using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using PolicyPilot.Api.Models;

namespace PolicyPilot.Tests;

public class PolicyApiTests : IClassFixture<PolicyPilotWebApplicationFactory>
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter() }
    };

    private readonly HttpClient _client;

    public PolicyApiTests(PolicyPilotWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetPolicies_ReturnsSeededPolicies()
    {
        var response = await _client.GetAsync("/policies");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var policies = await response.Content.ReadFromJsonAsync<List<Policy>>(JsonOptions);

        Assert.NotNull(policies);
        Assert.NotEmpty(policies);
        Assert.Contains(policies, policy => policy.PolicyNumber == "POL-1001");
    }

    [Fact]
    public async Task GetPolicy_WhenExists_ReturnsPolicy()
    {
        var response = await _client.GetAsync("/policies/1");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var policy = await response.Content.ReadFromJsonAsync<Policy>(JsonOptions);

        Assert.NotNull(policy);
        Assert.Equal(1, policy.Id);
        Assert.Equal("POL-1001", policy.PolicyNumber);
        Assert.Equal("Ava Patel", policy.HolderName);
    }

    [Fact]
    public async Task GetPolicy_WhenMissing_ReturnsNotFound()
    {
        var response = await _client.GetAsync("/policies/9999");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetPolicyClaims_WhenPolicyExists_ReturnsClaims()
    {
        var response = await _client.GetAsync("/policies/1/claims");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var claims = await response.Content.ReadFromJsonAsync<List<Claim>>(JsonOptions);

        Assert.NotNull(claims);
        Assert.NotEmpty(claims);
        Assert.All(claims, claim => Assert.Equal(1, claim.PolicyId));
    }

    [Fact]
    public async Task CreateClaim_WhenDescriptionMissing_ReturnsBadRequest()
    {
        var request = new
        {
            policyId = 1,
            claimDate = "2026-10-13",
            description = ""
        };

        var response = await _client.PostAsJsonAsync("/claims123", request);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task CreateClaim_WhenValid_ReturnsCreated()
    {
        var request = new
        {
            policyId = 1,
            claimDate = "2026-10-13",
            description = "Test claim from unit test"
        };

        var response = await _client.PostAsJsonAsync("/claims123", request);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var claim = await response.Content.ReadFromJsonAsync<Claim>(JsonOptions);

        Assert.NotNull(claim);
        Assert.Equal(1, claim.PolicyId);
        Assert.Equal("Test claim from unit test", claim.Description);
        Assert.Equal(ClaimStatus.Submitted, claim.Status);
    }

    [Fact]
    public async Task UpdatePolicyStatus_WhenExists_UpdatesStatus()
    {
        var request = new { status = "Cancelled" };

        var response = await _client.PutAsJsonAsync("/policies/2/status", request);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var policy = await response.Content.ReadFromJsonAsync<Policy>(JsonOptions);

        Assert.NotNull(policy);
        Assert.Equal(2, policy.Id);
        Assert.Equal(PolicyStatus.Cancelled, policy.Status);
    }
}
