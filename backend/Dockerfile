# Use the official .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["LearnerCenter.API.csproj", "./"]
RUN dotnet restore "LearnerCenter.API.csproj"

# Copy the rest of the source code
COPY . .

# Build the application
RUN dotnet build "LearnerCenter.API.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "LearnerCenter.API.csproj" -c Release -o /app/publish

# Use the official ASP.NET runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

# Copy the published app
COPY --from=publish /app/publish .

# Expose port 8080
EXPOSE 8080

# Set the entry point
ENTRYPOINT ["dotnet", "LearnerCenter.API.dll"]