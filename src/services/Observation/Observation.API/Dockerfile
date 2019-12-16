FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["src/Services/Observation/Observation.API/Observation.API.csproj", "src/Services/Observation/Observation.API/"]
RUN dotnet restore "src/Services/Observation/Observation.API/Observation.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Observation/Observation.API"
RUN dotnet build "Observation.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Observation.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Observation.API.dll"]