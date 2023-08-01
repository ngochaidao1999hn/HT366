#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["HT366.API/HT366.API.csproj", "HT366.API/"]
COPY ["HT366.Application/HT366.Application.csproj", "HT366.Application/"]
COPY ["HT366.Domain/HT366.Domain.csproj", "HT366.Domain/"]
COPY ["HT366.Infrastructure/HT366.Infrastructure.csproj", "HT366.Infrastructure/"]
RUN dotnet restore "HT366.API/HT366.API.csproj"
COPY . .
WORKDIR "/src/HT366.API"
RUN dotnet build "HT366.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HT366.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HT366.API.dll"]