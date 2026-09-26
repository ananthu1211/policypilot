using System.Text.Json.Serialization;

namespace PolicyPilot.Api.Models;

public class Policy
{
    public int Id { get; set; }
    public string PolicyNumber { get; set; } = "";
    public string HolderName { get; set; } = "";
    public PolicyType Type { get; set; }
    public PolicyStatus Status { get; set; }

    [JsonIgnore]
    public List<Claim> Claims { get; set; } = [];
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum PolicyType
{
    Auto,
    Home,
    Health,
    Life
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum PolicyStatus
{
    Active,
    Lapsed,
    Cancelled
}
