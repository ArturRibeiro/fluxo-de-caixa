FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Financeiro.Consolidado.Blazor.App/Financeiro.Consolidado.Blazor.App.csproj", "src/Financeiro.Consolidado.Blazor.App/"]
RUN dotnet restore "src/Financeiro.Consolidado.Blazor.App/Financeiro.Consolidado.Blazor.App.csproj"
COPY . .
WORKDIR "/src/src/Financeiro.Consolidado.Blazor.App"
RUN dotnet build "./Financeiro.Consolidado.Blazor.App.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Financeiro.Consolidado.Blazor.App.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Financeiro.Consolidado.Blazor.App.dll"]
