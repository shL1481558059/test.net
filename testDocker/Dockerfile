FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /apptest

WORKDIR /web
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /web
COPY --from=build /web/publish ./
ENTRYPOINT ["dotnet", "testDocker.dll"]