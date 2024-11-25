FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Orders/Orders.csproj", "Orders/"]
COPY ["Orders.BLL/Orders.BLL.csproj", "Orders.BLL/"]
COPY ["Orders.DAL/Orders.DAL.csproj", "Orders.DAL/"]
RUN dotnet restore "Orders/Orders.csproj"
COPY . .
WORKDIR "/src/Orders"
RUN dotnet build "Orders.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Orders.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Orders.dll"]
