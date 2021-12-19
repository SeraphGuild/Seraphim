FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG PROJECT_BUILD_CONFIGURATION
COPY . ./app
WORKDIR ./app/Seraphim
RUN dotnet restore "Seraphim.csproj"
RUN dotnet build "Seraphim.csproj" -c $PROJECT_BUILD_CONFIGURATION -o /build

FROM build AS publish
RUN dotnet publish "Seraphim.csproj" -c $PROJECT_BUILD_CONFIGURATION -o /publish

FROM base as final
COPY --from=publish ./publish .
ENTRYPOINT ["dotnet", "Seraphim.dll"]