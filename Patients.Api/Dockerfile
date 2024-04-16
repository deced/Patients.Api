FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Patients.Api/Patients.Api.csproj", "Patients.Api/"]
RUN dotnet restore "Patients.Api/Patients.Api.csproj"
COPY . .
WORKDIR "/src/Patients.Api"
RUN dotnet build "Patients.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Patients.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN apt-get -y update; apt-get -y install curl
ENTRYPOINT ["dotnet", "Patients.Api.dll"]
