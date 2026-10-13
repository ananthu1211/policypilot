# What I Learned This Week: Docker Basics & .NET Containerization

## 1. What is Docker & Open Source?

* **What is Docker?** Docker is an open-source platform that packages applications and their dependencies into standardized units called **containers**. It solves the classic *"It works on my machine!"* problem by ensuring code runs identically across local machines, staging environments, and production servers.
* **Core Concepts:**
  * **`Dockerfile`:** A text file containing step-by-step assembly instructions.
  * **Image:** A static, read-only blueprint containing binaries, runtimes, and dependencies.
  * **Container:** A live, running instance of an image running as an isolated process.
* **What is Open Source?** Software whose source code is publicly available for anyone to inspect, modify, and distribute. Anyone can modify Docker's source code locally, but changes to official releases require approval via Pull Requests from core maintainers.

## 2. Multi-Stage Builds & .NET SDK Dynamics

* **Why Multi-Stage Builds?** .NET requires a heavy SDK (`~800MB`) containing compilers (`csc`) and tools (`dotnet build`) to compile code, but only needs a small Runtime (`~200MB`) to execute compiled `.dll` files in production. Multi-stage builds use a temporary SDK container to compile the project, then copy **only** the published `.dll` files into a lightweight runtime image.
* **Why Docker Needs the SDK:** Containers start as blank slates with no pre-installed software. Unlike a local development machine with .NET pre-installed, Docker must pull the SDK image to execute `dotnet build` inside its isolated build environment.
* **Version Compatibility:** SDK images are backward compatible (e.g., .NET 8 SDK can build .NET 6 apps), but older SDK images cannot compile newer project target frameworks.

## 3. Steps Executed

1. **Created `.dockerignore`:** Excluded `.vs`, `bin`, `obj`, and `.git` folders to keep build contexts clean.
2. **Authored Multi-Stage `Dockerfile`:** Configured 4 stages (Runtime Base, SDK Build, Publish, Final) targeting .NET 8 for `PolicyPilot.Api`.
3. **Built Image:** Executed `docker build -t policypilot-api .` where `.` explicitly pointed Docker to parse the local `Dockerfile`.
4. **Ran Container:** Started the API on port 8080 using `docker run -d -p 8080:8080 --name policypilot-container policypilot-api`.
5. **Verified Container:** Verified that Swagger UI loaded at `http://localhost:8080/swagger`.
6. **Git Commit:** Committed the configuration with `git commit -m "Add Dockerfile"`.

## 4. Key Questions & Doubts Clarified

* **"We wrote a Dockerfile but didn't explicitly reference it in our commands?"**
  * *Answer:* The `.` in `docker build -t policypilot-api .` tells Docker to look in the current working directory for a file named `Dockerfile` by default.
* **"We ran a single command, so how did multi-stage build run?"**
  * *Answer:* Running `docker build .` triggers Docker to automatically parse and execute all stages in the `Dockerfile` sequentially as a single automated process.
* **"Why do I only see one final image in Docker Desktop?"**
  * *Answer:* Docker automatically cleans up and discards intermediate SDK containers once compiled binaries are copied into the final runtime image.
* **"Can't I just compile locally and copy output into a single-stage Dockerfile?"**
  * *Answer:* Yes, but local compiles risk OS/SDK version mismatches ("works on my machine") and require build tools installed on every developer machine and CI/CD server. Multi-stage builds guarantee consistent compilation inside identical Linux environments.
* **"If I add a second project, do I download the SDK again?"**
  * *Answer:* No. Docker caches base images locally. The SDK image is downloaded once and reused across multiple project builds.