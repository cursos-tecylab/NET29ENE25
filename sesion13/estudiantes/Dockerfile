    FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
    WORKDIR /app
    EXPOSE 6666

    ENV ASPNETCORE_URLS=http://+:6666
    FROM mcr.microsoft.com/dotnet/sdk:8.0.301 AS build
    ARG configuration=Release
    WORKDIR /src
    COPY ["src/Estudiantes/Estudiantes.Api/Estudiantes.Api.csproj", "src/Estudiantes/Estudiantes.Api/"]
    RUN dotnet restore "src\Estudiantes\Estudiantes.Api\Estudiantes.Api.csproj"
    COPY . .
    WORKDIR "/src/src/Estudiantes/Estudiantes.Api"
    RUN dotnet build "Estudiantes.Api.csproj" -c $configuration -o /app/build

    FROM build AS publish
    ARG configuration=Release
    RUN dotnet publish "Estudiantes.Api.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

    FROM base AS final
    WORKDIR /app
    COPY --from=publish /app/publish .

    ENTRYPOINT ["dotnet", "Estudiantes.Api.dll"]