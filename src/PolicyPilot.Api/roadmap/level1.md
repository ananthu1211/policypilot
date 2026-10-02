Week 1 — Foundation (Level 0)

Summary
Built a working ASP.NET Core Web API with EF Core and SQLite, containerized in Docker, with daily commits and a documented plan.

Day by day
Monday: Created repo, wrote project plan in README
Tuesday: Scaffolded ASP.NET Core Web API project, confirmed Swagger UI loads
Wednesday: Added EF Core and SQLite, created Policy and Claim models, ran first migration
Thursday: Implemented read endpoints (GET policies, GET policy by id, GET claims for a policy)
Friday: Implemented write endpoints (POST claim, PUT policy status), added seed data
Saturday: Learned Docker basics, wrote a Dockerfile, built and ran the app in a container
Sunday: Fixed remaining issues, updated README, wrote learning notes, read about Azure App Service

Topics covered
ASP.NET Core Web API structure and routing
REST API design (GET, POST, PUT)
Entity Framework Core and database migrations
Relational data modeling (one policy to many claims)
Postgree as a local database
Seed data for testing
Docker: images, Dockerfiles, build and run
Git and GitHub workflow with incremental commits
Documentation habits (README as a living project plan)

Not covered yet, on purpose
Azure deployment — planned for Week 2
AI and LLM concepts — planned for Level 1
Security, authentication, Key Vault — planned for Level 2
CI/CD pipeline — planned for Week 2

Status: Week 1 complete. Moving to Week 2 — Azure deployment.