#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["NewsAndMedia.WorkerService/NewsAndMedia.WorkerService.csproj", "NewsAndMedia.WorkerService/"]
COPY ["NewsAndMedia.Core/NewsAndMedia.Core.csproj", "NewsAndMedia.Core/"]
COPY ["NewsAndMedia.Model/NewsAndMedia.Model.csproj", "NewsAndMedia.Model/"]
COPY ["NewsAndMedia.Infrastructure/NewsAndMedia.Infrastructure.csproj", "NewsAndMedia.Infrastructure/"]
RUN dotnet restore "./NewsAndMedia.WorkerService/./NewsAndMedia.WorkerService.csproj"
COPY . .
WORKDIR "/src/NewsAndMedia.WorkerService"
RUN dotnet build "./NewsAndMedia.WorkerService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./NewsAndMedia.WorkerService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NewsAndMedia.WorkerService.dll"]