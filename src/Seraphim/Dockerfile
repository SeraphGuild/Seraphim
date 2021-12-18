#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
ARG AZURE_CLIENT_SECRET
ARG PROJECT_BUILD_CONFIGURATION
ENV AZURE_CLIENT_ID 0437c411-850d-4961-aee4-77842460a6cd
ENV AZURE_CLIENT_SECRET $AZURE_CLIENT_SECRET
ENV AZURE_TENANT_ID d805168c-58f5-47d8-bf88-878e9bad98dc
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src/Seraphim
RUN dotnet restore "Seraphim.csproj"
RUN dotnet build "Seraphim.csproj" -c $PROJECT_BUILD_CONFIGURATION -o /seraphim/build

FROM build AS publish
RUN dotnet publish "Seraphim.csproj" -c $PROJECT_BUILD_CONFIGURATION -o /seraphim/publish

FROM base AS final
COPY --from=publish /seraphim/publish .
ENTRYPOINT ["dotnet", "Seraphim.dll"]