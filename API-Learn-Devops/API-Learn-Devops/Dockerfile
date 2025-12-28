# Backend Dockerfile (ASP.NET Core .NET 8)
# Multi-stage build: restore/build/publish -> runtime

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project files and restore
COPY ./API-Learn-Devops.csproj ./
RUN dotnet restore "API-Learn-Devops.csproj"

# Copy all source and publish
COPY . .
RUN dotnet publish "API-Learn-Devops.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Bind to HTTP 8080 inside container
ENV ASPNETCORE_URLS=http://+:8080

COPY --from=build /app/publish .
EXPOSE 8080

ENTRYPOINT ["dotnet", "API-Learn-Devops.dll"]
