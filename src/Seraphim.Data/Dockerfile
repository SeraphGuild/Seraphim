FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS restore
WORKDIR /src
COPY Seraphim.Data.csproj .
RUN dotnet restore Seraphim.Data.csproj

FROM restore AS build
ARG PROJECT_BUILD_CONFIGURATION
COPY . .
RUN dotnet build Seraphim.Data.csproj -c $PROJECT_BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish Seraphim.Data.csproj -c $PROJECT_BUILD_CONFIGURATION -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Seraphim.Data.dll"]