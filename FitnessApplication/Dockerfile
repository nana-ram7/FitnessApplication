# Base runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy .csproj and restore
COPY FitnessApplication/FitnessApplication.csproj FitnessApplication/
RUN dotnet restore "FitnessApplication/FitnessApplication.csproj"

# Copy the rest of the app
COPY FitnessApplication/ FitnessApplication/
WORKDIR /src/FitnessApplication
RUN dotnet build "FitnessApplication.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "FitnessApplication.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final runtime stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FitnessApplication.dll"]

#updated dockerfile 
