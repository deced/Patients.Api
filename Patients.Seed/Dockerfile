FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Patients.Seed/Patients.Seed.csproj", "Patients.Seed/"]
RUN dotnet restore "Patients.Seed/Patients.Seed.csproj"
COPY . .
WORKDIR "/src/Patients.Seed"
RUN dotnet build "Patients.Seed.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Patients.Seed.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Patients.Seed.dll"]
