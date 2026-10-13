# ---------------------------------------------------------
# STAGE 1: Runtime Base
# ---------------------------------------------------------
    FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
    WORKDIR /app
    EXPOSE 8080
    EXPOSE 8081
    
    # Set ASP.NET Core environment to Development to ensure Swagger runs inside the container
    ENV ASPNETCORE_ENVIRONMENT=Development
    ENV ASPNETCORE_URLS=http://+:8080
    
    # ---------------------------------------------------------
    # STAGE 2: Build & Restore (SDK)
    # ---------------------------------------------------------
    FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
    ARG BUILD_CONFIGURATION=Release
    WORKDIR /src
    
    # Copy project file and restore dependencies
    COPY ["PolicyPilot.Api.csproj", "./"]
    RUN dotnet restore "PolicyPilot.Api.csproj"
    
    # Copy all source files and compile
    COPY . .
    RUN dotnet build "PolicyPilot.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build
    
    # ---------------------------------------------------------
    # STAGE 3: Publish
    # ---------------------------------------------------------
    FROM build AS publish
    ARG BUILD_CONFIGURATION=Release
    RUN dotnet publish "PolicyPilot.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
    
    # ---------------------------------------------------------
    # STAGE 4: Final Runtime Image
    # ---------------------------------------------------------
    FROM base AS final
    WORKDIR /app
    COPY --from=publish /app/publish .
    ENTRYPOINT ["dotnet", "PolicyPilot.Api.dll"]