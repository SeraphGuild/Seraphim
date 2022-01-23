#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG PROJECT_BUILD_CONFIGURATION
WORKDIR /src
COPY ["Seraphim.Data/Seraphim.Data.csproj", "Seraphim.Data/"]
RUN dotnet restore "Seraphim.Data/Seraphim.Data.csproj"
COPY . .
WORKDIR "/src/Seraphim.Data"
RUN dotnet build "Seraphim.Data.csproj" -c $PROJECT_BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "Seraphim.Data.csproj" -c $PROJECT_BUILD_CONFIGURATION -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Seraphim.Data.dll"]