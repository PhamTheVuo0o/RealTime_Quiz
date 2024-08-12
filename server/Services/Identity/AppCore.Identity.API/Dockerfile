#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 21101
EXPOSE 20001

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Identity/AppCore.Identity.API/AppCore.Identity.API.csproj", "Services/Identity/AppCore.Identity.API/"]
RUN dotnet restore "Services/Identity/AppCore.Identity.API/AppCore.Identity.API.csproj"
COPY . .
WORKDIR "/src/Services/Identity/AppCore.Identity.API"
RUN dotnet build "AppCore.Identity.API.csproj" -c DockerDev -o /app/build

FROM build AS publish
RUN dotnet publish "AppCore.Identity.API.csproj" -c DockerDev -o /app/publish /p:UseAppHost=false

# final images    
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS http://*:20001
ENV ASPNETCORE_ENVIRONMENT DockerDev
EXPOSE 21101
EXPOSE 20001
ENTRYPOINT ["dotnet", "AppCore.Identity.API.dll"]