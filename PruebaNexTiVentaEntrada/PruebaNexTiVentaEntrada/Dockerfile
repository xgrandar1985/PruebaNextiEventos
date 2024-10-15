

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["PruebaNexTiVentaEntrada/PruebaNexTiVentaEntrada.csproj", "PruebaNexTiVentaEntrada/"]
RUN dotnet restore "./PruebaNexTiVentaEntrada/PruebaNexTiVentaEntrada.csproj"
COPY . .
WORKDIR "/src/PruebaNexTiVentaEntrada"
RUN dotnet build "./PruebaNexTiVentaEntrada.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PruebaNexTiVentaEntrada.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PruebaNexTiVentaEntrada.dll"]