using Microsoft.EntityFrameworkCore;
using PolicyPilot.Api.Models;

namespace PolicyPilot.Api.Data;

public static class PolicySeed
{
    public static void Apply(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Policy>().HasData(
            new Policy { Id = 1, PolicyNumber = "POL-1001", HolderName = "Ava Patel", Type = PolicyType.Auto, Status = PolicyStatus.Active },
            new Policy { Id = 2, PolicyNumber = "POL-1002", HolderName = "Liam Chen", Type = PolicyType.Home, Status = PolicyStatus.Active },
            new Policy { Id = 3, PolicyNumber = "POL-1003", HolderName = "Noah Williams", Type = PolicyType.Health, Status = PolicyStatus.Active },
            new Policy { Id = 4, PolicyNumber = "POL-1004", HolderName = "Emma Garcia", Type = PolicyType.Life, Status = PolicyStatus.Lapsed },
            new Policy { Id = 5, PolicyNumber = "POL-1005", HolderName = "Olivia Brown", Type = PolicyType.Auto, Status = PolicyStatus.Active },
            new Policy { Id = 6, PolicyNumber = "POL-1006", HolderName = "Mason Davis", Type = PolicyType.Home, Status = PolicyStatus.Cancelled },
            new Policy { Id = 7, PolicyNumber = "POL-1007", HolderName = "Sophia Miller", Type = PolicyType.Health, Status = PolicyStatus.Active },
            new Policy { Id = 8, PolicyNumber = "POL-1008", HolderName = "James Wilson", Type = PolicyType.Life, Status = PolicyStatus.Active },
            new Policy { Id = 9, PolicyNumber = "POL-1009", HolderName = "Isabella Moore", Type = PolicyType.Auto, Status = PolicyStatus.Lapsed },
            new Policy { Id = 10, PolicyNumber = "POL-1010", HolderName = "Ethan Taylor", Type = PolicyType.Home, Status = PolicyStatus.Active },
            new Policy { Id = 11, PolicyNumber = "POL-1011", HolderName = "Mia Anderson", Type = PolicyType.Health, Status = PolicyStatus.Active },
            new Policy { Id = 12, PolicyNumber = "POL-1012", HolderName = "Lucas Thomas", Type = PolicyType.Life, Status = PolicyStatus.Cancelled }
        );

        modelBuilder.Entity<Claim>().HasData(
            new Claim { Id = 1, PolicyId = 1, ClaimDate = new DateOnly(2026, 1, 14), Description = "Rear bumper damage from a parking lot collision.", Status = ClaimStatus.Approved },
            new Claim { Id = 2, PolicyId = 1, ClaimDate = new DateOnly(2026, 6, 2), Description = "Windshield cracked by highway debris.", Status = ClaimStatus.UnderReview },
            new Claim { Id = 3, PolicyId = 2, ClaimDate = new DateOnly(2026, 2, 8), Description = "Kitchen water leak damaged the floor.", Status = ClaimStatus.Submitted },
            new Claim { Id = 4, PolicyId = 3, ClaimDate = new DateOnly(2025, 11, 19), Description = "Emergency room visit for a broken wrist.", Status = ClaimStatus.Approved },
            new Claim { Id = 5, PolicyId = 3, ClaimDate = new DateOnly(2026, 4, 27), Description = "Follow-up physical therapy sessions.", Status = ClaimStatus.UnderReview },
            new Claim { Id = 6, PolicyId = 4, ClaimDate = new DateOnly(2025, 9, 3), Description = "Beneficiary update requested after a name change.", Status = ClaimStatus.Rejected },
            new Claim { Id = 7, PolicyId = 5, ClaimDate = new DateOnly(2026, 3, 11), Description = "Hail damage to the hood and roof.", Status = ClaimStatus.Submitted },
            new Claim { Id = 8, PolicyId = 5, ClaimDate = new DateOnly(2026, 7, 21), Description = "Side mirror replaced after a sideswipe.", Status = ClaimStatus.Approved },
            new Claim { Id = 9, PolicyId = 6, ClaimDate = new DateOnly(2025, 12, 16), Description = "Storm lifted shingles off the garage.", Status = ClaimStatus.Rejected },
            new Claim { Id = 10, PolicyId = 7, ClaimDate = new DateOnly(2026, 5, 9), Description = "Annual specialist consultation.", Status = ClaimStatus.Approved },
            new Claim { Id = 11, PolicyId = 8, ClaimDate = new DateOnly(2026, 1, 30), Description = "Accidental death rider documentation review.", Status = ClaimStatus.UnderReview },
            new Claim { Id = 12, PolicyId = 8, ClaimDate = new DateOnly(2026, 8, 4), Description = "Waiver of premium request during disability.", Status = ClaimStatus.Submitted },
            new Claim { Id = 13, PolicyId = 9, ClaimDate = new DateOnly(2025, 10, 22), Description = "Towed after an engine failure on the freeway.", Status = ClaimStatus.Approved },
            new Claim { Id = 14, PolicyId = 10, ClaimDate = new DateOnly(2026, 2, 18), Description = "Burst pipe in the basement.", Status = ClaimStatus.UnderReview },
            new Claim { Id = 15, PolicyId = 11, ClaimDate = new DateOnly(2026, 6, 15), Description = "Prescription reimbursement for a covered condition.", Status = ClaimStatus.Submitted }
        );
    }
}
