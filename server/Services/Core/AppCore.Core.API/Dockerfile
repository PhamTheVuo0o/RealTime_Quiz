#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 20002
EXPOSE 21102

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Core/AppCore.Core.API/AppCore.Core.API.csproj", "Services/Core/AppCore.Core.API/"]
RUN dotnet restore "./Services/Core/AppCore.Core.API/AppCore.Core.API.csproj"
COPY . .
WORKDIR "/src/Services/Core/AppCore.Core.API"
RUN dotnet build "./AppCore.Core.API.csproj" -c DockerDev -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AppCore.Core.API.csproj" -c DockerDev -o /app/publish /p:UseAppHost=false

#final image    
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS http://*:20002
ENV ASPNETCORE_ENVIRONMENT=DockerDev
EXPOSE 20002
EXPOSE 21102
ENTRYPOINT ["dotnet", "AppCore.Core.API.dll"]