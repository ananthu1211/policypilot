using System.Text.Json.Serialization;

namespace PolicyPilot.Api.Models;

public class Claim
{
    public int Id { get; set; }
    public int PolicyId { get; set; }
    public DateOnly ClaimDate { get; set; }
    public string Description { get; set; } = "";
    public ClaimStatus Status { get; set; }

    [JsonIgnore]
    public Policy? Policy { get; set; }
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ClaimStatus
{
    Submitted,
    UnderReview,
    Approved,
    Rejected
}
