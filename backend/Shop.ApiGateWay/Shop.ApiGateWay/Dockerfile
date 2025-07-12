FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5019

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Shop.ApiGateWay/Shop.ApiGateWay.csproj", "Shop.ApiGateWay/"]
RUN dotnet restore "Shop.ApiGateWay/Shop.ApiGateWay.csproj"
COPY . .
WORKDIR "/src/Shop.ApiGateWay"
RUN dotnet build "Shop.ApiGateWay.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Shop.ApiGateWay.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Shop.ApiGateWay.dll"]
