#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["testDocker/testDocker.csproj", "testDocker/"]
RUN dotnet restore "testDocker/testDocker.csproj"
COPY . .
WORKDIR "/src/testDocker"
RUN dotnet build "testDocker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "testDocker.csproj" -c Release -o /app/publish /p:UseAppHost=false
ADD ./bin/Release/netcoreapp1.1/centos.7-x64/publish /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
WORKDIR /app # IMPORTANT!

ENTRYPOINT ["dotnet", "testDocker.dll"]