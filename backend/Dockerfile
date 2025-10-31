FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["LearnerCenter.API.csproj", "."]
RUN dotnet restore "./LearnerCenter.API.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "LearnerCenter.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LearnerCenter.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LearnerCenter.API.dll"]