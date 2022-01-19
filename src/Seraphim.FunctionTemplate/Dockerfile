FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR app
COPY ./Seraphim.TeamManagement /app
RUN dotnet publish *.csproj --output /home/site/wwwroot

FROM mcr.microsoft.com/azure-functions/dotnet:4
ENV AzureWebJobsScriptRoot=/home/site/wwwroot
ENV AzureWebJobsStorage=UseDevelopmentStorage=true
ENV FUNCTIONS_WORKER_RUNTIME=dotnet
ENV AzureFunctionsJobHost__Logging__Console__IsEnabled=true
COPY --from=build ["/home/site/wwwroot", "/home/site/wwwroot"]