#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 20000
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/ApiGateway/ApiGateway.csproj", "Services/ApiGateway/"]
RUN dotnet restore "./Services/ApiGateway/ApiGateway.csproj"
COPY . .
WORKDIR "/src/Services/ApiGateway"
RUN dotnet build "./ApiGateway.csproj" -c DockerDev -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=DockerDev
RUN dotnet publish "./ApiGateway.csproj" -c DockerDev -o /app/publish /p:UseAppHost=false

#final images  
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS http://*:20000
ENV ASPNETCORE_ENVIRONMENT DockerDev
EXPOSE 20000
EXPOSE 443
ENTRYPOINT ["dotnet", "ApiGateway.dll"]