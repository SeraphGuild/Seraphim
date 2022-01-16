FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
COPY . ./src
WORKDIR ./src/Seraphim.Interactions
RUN mkdir -p /home/site/wwwroot
RUN dotnet restore "Seraphim.Interactions.csproj"
RUN dotnet build "Seraphim.Interactions.csproj" -o /build

FROM build AS publish
RUN dotnet publish "Seraphim.Interactions.csproj" -o /home/site/wwwroot

FROM mcr.microsoft.com/azure-functions/dotnet:4
ENV AzureWebJobsScriptRoot=/home/site/wwwroot
ENV AzureFunctionsJobHost__Logging__Console__IsEnabled=true
COPY --from=publish ["/home/site/wwwroot", "/home/site/wwwroot"]