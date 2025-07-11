FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5022

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Shop.ArticleService/Shop.ArticleService.csproj", "Shop.ArticleService/"]
RUN dotnet restore "Shop.ArticleService/Shop.ArticleService.csproj"
COPY . .
WORKDIR "/src/Shop.ArticleService"
RUN dotnet build "Shop.ArticleService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Shop.ArticleService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Shop.ArticleService.dll"]
