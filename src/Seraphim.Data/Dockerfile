FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG PROJECT_BUILD_CONFIGURATION
COPY . ./app
WORKDIR ./app/Seraphim.Data
RUN dotnet publish "Seraphim.Data.csproj" -c $PROJECT_BUILD_CONFIGURATION

FROM base AS final
COPY --from=build ./bin .
ENTRYPOINT ["dotnet", "Seraphim.Data.dll"]