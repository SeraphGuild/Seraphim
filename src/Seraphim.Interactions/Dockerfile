FROM mcr.microsoft.com/dotnet/sdk:6.0 AS restore
WORKDIR /src
COPY Seraphim.Interactions/Seraphim.Interactions.csproj Seraphim.Interactions/
RUN dotnet restore Seraphim.Interactions/Seraphim.Interactions.csproj

FROM restore AS build
ARG PROJECT_BUILD_CONFIGURATION
COPY Seraphim.Interactions/ Seraphim.Interactions/
WORKDIR /src/Seraphim.Interactions
RUN dotnet build Seraphim.Interactions.csproj -c $PROJECT_BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish Seraphim.Interactions.csproj -c $PROJECT_BUILD_CONFIGURATION -o /app/publish

FROM mcr.microsoft.com/azure-functions/dotnet:4 AS base
WORKDIR /home/site/wwwroot
COPY --from=publish /app/publish .
# for some dumb reason, libsodium cannot be found unless the .so is directly in the bin directory
COPY bin/runtimes/linux-x64/native/libsodium.so bin/libsodium.so