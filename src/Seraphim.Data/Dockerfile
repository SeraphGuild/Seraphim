FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG PROJECT_BUILD_CONFIGURATION
COPY . ./app
WORKDIR ./app/Seraphim.Data
RUN dotnet restore "Seraphim.Data.csproj"
RUN dotnet build "Seraphim.Data.csproj" -c $PROJECT_BUILD_CONFIGURATION -o /build

FROM build AS publish
RUN dotnet publish "Seraphim.Data.csproj" -c $PROJECT_BUILD_CONFIGURATION -o /publish

FROM base AS final
COPY --from=publish ./publish .
ENTRYPOINT ["dotnet", "Seraphim.Data.dll"]