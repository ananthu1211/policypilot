using Microsoft.EntityFrameworkCore;
using PolicyPilot.Api.Data;
using PolicyPilot.Api.Models;

namespace PolicyPilot.Api.Endpoints;

public static class PolicyEndpoints
{
    public static void MapPolicyEndpoints(this WebApplication app)
    {
        app.MapGet("/policies", async (PolicyPilotDbContext db) =>
            await db.Policies.AsNoTracking().OrderBy(policy => policy.Id).ToListAsync())
            .WithName("GetPolicies")
            .WithTags("Policies");

        app.MapGet("/policies/{id:int}", async (int id, PolicyPilotDbContext db) =>
            await db.Policies.AsNoTracking().FirstOrDefaultAsync(policy => policy.Id == id)
                is { } policy
                    ? Results.Ok(policy)
                    : Results.NotFound())
            .WithName("GetPolicy")
            .WithTags("Policies");

        app.MapGet("/policies/{id:int}/claims", async (int id, PolicyPilotDbContext db) =>
        {
            var policyExists = await db.Policies.AnyAsync(policy => policy.Id == id);
            if (!policyExists)
            {
                return Results.NotFound();
            }

            var claims = await db.Claims.AsNoTracking()
                .Where(claim => claim.PolicyId == id)
                .OrderBy(claim => claim.ClaimDate)
                .ToListAsync();

            return Results.Ok(claims);
        })
        .WithName("GetPolicyClaims")
        .WithTags("Claims");

        app.MapPost("/claim", async (CreateClaimRequest request, PolicyPilotDbContext db) =>
        {
            if (string.IsNullOrWhiteSpace(request.Description))
            {
                return Results.BadRequest(new { message = "Description is required." });
            }

            var policyExists = await db.Policies.AnyAsync(policy => policy.Id == request.PolicyId);
            if (!policyExists)
            {
                return Results.NotFound(new { message = "Policy was not found." });
            }

            var claim = new Claim
            {
                PolicyId = request.PolicyId,
                ClaimDate = request.ClaimDate,
                Description = request.Description.Trim(),
                Status = request.Status ?? ClaimStatus.Submitted
            };

            db.Claims.Add(claim);
            await db.SaveChangesAsync();

            return Results.Created($"/policies/{claim.PolicyId}/claims", claim);
        })
        .WithName("CreateClaim")
        .WithTags("Claims");

        app.MapPut("/policies/{id:int}/status", async (int id, UpdatePolicyStatusRequest request, PolicyPilotDbContext db) =>
        {
            var policy = await db.Policies.FirstOrDefaultAsync(item => item.Id == id);
            if (policy is null)
            {
                return Results.NotFound();
            }

            policy.Status = request.Status;
            await db.SaveChangesAsync();

            return Results.Ok(policy);
        })
        .WithName("UpdatePolicyStatus")
        .WithTags("Policies");
    }
}

public record CreateClaimRequest(int PolicyId, DateOnly ClaimDate, string Description, ClaimStatus? Status);

public record UpdatePolicyStatusRequest(PolicyStatus Status);
