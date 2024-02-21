FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Routes.Web/Routes.Web.csproj", "Routes.Web/"]
COPY ["Routes.UseCases/Routes.UseCases.csproj", "Routes.UseCases/"]
COPY ["Routes.Infrastructure.Abstractions/Routes.Infrastructure.Abstractions.csproj", "Routes.Infrastructure.Abstractions/"]
COPY ["Routes.Infrastructure.Implementations/Routes.Infrastructure.Implementations.csproj", "Routes.Infrastructure.Implementations/"]
RUN dotnet restore "Routes.Web/Routes.Web.csproj"
COPY . .
WORKDIR "/src/Routes.Web"
RUN dotnet build "Routes.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Routes.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Routes.Web.dll"]
