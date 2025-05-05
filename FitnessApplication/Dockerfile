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