#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base

RUN ln -sf /usr/share/zoneinfo/Europe/Istanbul /etc/localtime 
RUN date

WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Trader.TradeService/Trader.TradeService.csproj", "src/Trader.TradeService/"]
COPY ["src/BuildingBlocks/BuildingBlocks.Core/BuildingBlocks.Core.csproj", "src/BuildingBlocks/BuildingBlocks.Core/"]
COPY ["src/BuildingBlocks/BuildingBlocks.Data/BuildingBlocks.Data.csproj", "src/BuildingBlocks/BuildingBlocks.Data/"]
COPY ["src/BuildingBlocks/BuildingBlocks.Event/BuildingBlocks.Event.csproj", "src/BuildingBlocks/BuildingBlocks.Event/"]
RUN dotnet restore "src/Trader.TradeService/Trader.TradeService.csproj"
COPY . .
WORKDIR "/src/src/Trader.TradeService"
RUN dotnet build "Trader.TradeService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Trader.TradeService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Trader.TradeService.dll"]