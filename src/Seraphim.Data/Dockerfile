FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG PROJECT_BUILD_CONFIGURATION
WORKDIR /app
COPY ./Seraphim.Data /app
RUN dotnet publish *.csproj -c $PROJECT_BUILD_CONFIGURATION

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
EXPOSE 80
EXPOSE 443
COPY --from=build ./bin .
ENTRYPOINT ["dotnet", "Seraphim.Data.dll"]