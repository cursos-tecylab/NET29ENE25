    FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
    WORKDIR /app
    EXPOSE 7777

    ENV ASPNETCORE_URLS=http://+:8888
    FROM mcr.microsoft.com/dotnet/sdk:9.0.102 AS build
    ARG configuration=Release
    WORKDIR /src
    COPY ["src/Docentes/Docentes.Api/Docentes.api.csproj", "src/Docentes/Docentes.Api/"]
    RUN dotnet restore "src\Docentes\Docentes.Api\Docentes.api.csproj"
    COPY . .
    WORKDIR "/src/src/Docentes/Docentes.Api"
    RUN dotnet build "Docentes.api.csproj" -c $configuration -o /app/build

    FROM build AS publish
    ARG configuration=Release
    RUN dotnet publish "Docentes.api.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

    FROM base AS final
    WORKDIR /app
    COPY --from=publish /app/publish .

    ENTRYPOINT ["dotnet", "Docentes.api.dll"]