FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["aplicacao-web/aplicacao-web.csproj", "aplicacao-web/"]
RUN dotnet restore "aplicacao-web/aplicacao-web.csproj"
COPY . .
WORKDIR "/src/aplicacao-web"
RUN dotnet build "aplicacao-web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "aplicacao-web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "aplicacao-web.dll"] 