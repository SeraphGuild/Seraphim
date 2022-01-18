FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
COPY . ./src
WORKDIR ./src/Seraphim.Interactions
RUN dotnet publish "Seraphim.Interactions.csproj" -o /home/site/wwwroot

FROM mcr.microsoft.com/azure-functions/dotnet:4
ENV AzureWebJobsScriptRoot=/home/site/wwwroot
ENV AzureFunctionsJobHost__Logging__Console__IsEnabled=true
COPY --from=build ["/home/site/wwwroot", "/home/site/wwwroot"]