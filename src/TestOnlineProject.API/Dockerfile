#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TestOnlineProject.API/TestOnlineProject.API.csproj", "TestOnlineProject.API/"]
COPY ["TestOnlineProject.Application/TestOnlineProject.Application.csproj", "TestOnlineProject.Application/"]
COPY ["TestOnlineProject.Infrastructure/TestOnlineProject.Infrastructure.csproj", "TestOnlineProject.Infrastructure/"]
COPY ["TestOnlineProject.Domain/TestOnlineProject.Domain.csproj", "TestOnlineProject.Domain/"]
RUN dotnet restore "TestOnlineProject.API/TestOnlineProject.API.csproj"
COPY . .
WORKDIR "/src/TestOnlineProject.API"
RUN dotnet build "TestOnlineProject.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TestOnlineProject.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestOnlineProject.API.dll"]