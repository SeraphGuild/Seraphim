FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG PROJECT_BUILD_CONFIGURATION
WORKDIR app
COPY ./Seraphim.InteractionAcknowledgement /app
RUN dotnet publish *.csproj --output /home/site/wwwroot -c $PROJECT_BUILD_CONFIGURATION
RUN cp /home/site/wwwroot/bin/runtimes/linux-x64/native/libsodium.so /home/site/wwwroot/bin/libsodium.so

FROM mcr.microsoft.com/azure-functions/dotnet:4
ENV AzureWebJobsScriptRoot=/home/site/wwwroot
ENV AzureWebJobsStorage=UseDevelopmentStorage=true
ENV FUNCTIONS_WORKER_RUNTIME=dotnet
ENV AzureFunctionsJobHost__Logging__Console__IsEnabled=true
COPY --from=build ["/home/site/wwwroot", "/home/site/wwwroot"]