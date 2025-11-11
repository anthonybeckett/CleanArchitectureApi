FROM mcr.microsoft.com/dotnet/sdk:10.0-alpine3.22-aot AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0-alpine3.22-aot AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/CleanArchitectureApi.Api/CleanArchitectureApi.Api.csproj", "src/CleanArchitectureApi.Api/"]
RUN dotnet restore "src/CleanArchitectureApi.Api/CleanArchitectureApi.Api.csproj"
COPY . .
WORKDIR "/src/src/CleanArchitectureApi.Api"
RUN dotnet build "CleanArchitectureApi.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "CleanArchitectureApi.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CleanArchitectureApi.Api.dll"]
