# Development target
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS dev

WORKDIR /src

EXPOSE 8080
EXPOSE 8081

ENV ASPNETCORE_ENVIRONMENT=Development
ENV DOTNET_USE_POLLING_FILE_WATCHER=1

CMD ["dotnet", "watch", "run", "--project", "src/CleanArchitectureApi.Api/CleanArchitectureApi.Api.csproj", "--urls=http://0.0.0.0:8080"]


# Runtime base
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS base

USER $APP_UID

WORKDIR /app

EXPOSE 8080
EXPOSE 8081


# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

ARG BUILD_CONFIGURATION=Release

WORKDIR /src

COPY ["src/CleanArchitectureApi.Api/CleanArchitectureApi.Api.csproj", "src/CleanArchitectureApi.Api/"]

RUN dotnet restore "src/CleanArchitectureApi.Api/CleanArchitectureApi.Api.csproj"

COPY . .

WORKDIR "/src/src/CleanArchitectureApi.Api"

RUN dotnet build "CleanArchitectureApi.Api.csproj" \
    -c $BUILD_CONFIGURATION \
    -o /app/build


# Publish stage
FROM build AS publish

ARG BUILD_CONFIGURATION=Release

RUN dotnet publish "CleanArchitectureApi.Api.csproj" \
    -c $BUILD_CONFIGURATION \
    -o /app/publish \
    /p:UseAppHost=false


# Production image
FROM base AS final

WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "CleanArchitectureApi.Api.dll"]