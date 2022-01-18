FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Build requires 3.1 SDK
# COPY --from=mcr.microsoft.com/dotnet/core/sdk:3.1 /usr/share/dotnet /usr/share/dotnet

WORKDIR app
COPY ./Seraphim.FunctionTemplate /app
RUN dotnet publish Seraphim.FunctionTemplate.csproj --output /home/site/wwwroot

FROM mcr.microsoft.com/azure-functions/dotnet:4
ENV AzureWebJobsScriptRoot=/home/site/wwwroot
ENV AzureWebJobsStorage=UseDevelopmentStorage=true
ENV FUNCTIONS_WORKER_RUNTIME=dotnet
ENV AzureFunctionsJobHost__Logging__Console__IsEnabled=true

COPY --from=build ["/home/site/wwwroot", "/home/site/wwwroot"]