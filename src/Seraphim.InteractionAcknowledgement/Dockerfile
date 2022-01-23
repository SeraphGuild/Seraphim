FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG PROJECT_BUILD_CONFIGURATION
WORKDIR /app
COPY ./Seraphim.InteractionAcknowledgement /app
RUN dotnet publish *.csproj --output /home/site/wwwroot -c $PROJECT_BUILD_CONFIGURATION
# for some dumb reason, libsodium cannot be found unless the .so is directly in the bin directory
RUN cp /home/site/wwwroot/bin/runtimes/linux-x64/native/libsodium.so /home/site/wwwroot/bin/libsodium.so

FROM mcr.microsoft.com/azure-functions/dotnet:4
COPY --from=build ["/home/site/wwwroot", "/home/site/wwwroot"]