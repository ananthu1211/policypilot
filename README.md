PolicyPilot
An AI-assisted insurance policy and claims system, built to learn Azure architecture and AI engineering end-to-end, in C#/.NET.

Level 0 — Foundation (in progress)
Plain API + database, deployed to Azure, with CI/CD and monitoring.

Data model
Policy: Id, PolicyNumber, HolderName, Type (Auto/Home/Health/Life), Status (Active/Lapsed/Cancelled)
Claim: Id, PolicyId (FK), ClaimDate, Description, Status (Submitted/UnderReview/Approved/Rejected)

Planned endpoints (Level 0)
GET /policies — list all policies
GET /policies/{id} — get one policy
GET /policies/{id}/claims — get claims for a policy
POST /claims — file a new claim
PUT /policies/{id}/status — update policy status

Stack
C# / .NET, EF Core, SQLite (local) then Azure SQL (deployed), Docker, GitHub Actions
