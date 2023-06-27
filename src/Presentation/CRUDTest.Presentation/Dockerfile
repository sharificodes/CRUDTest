#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Presentation/CRUDTest.Presentation/CRUDTest.Presentation.csproj", "src/Presentation/CRUDTest.Presentation/"]
COPY ["src/Core/CRUDTest.Application/CRUDTest.Application.csproj", "src/Core/CRUDTest.Application/"]
COPY ["src/Core/CRUDTest.Domain/CRUDTest.Domain.csproj", "src/Core/CRUDTest.Domain/"]
COPY ["src/Infrastructure/CRUDTest.Infrastructure/CRUDTest.Infrastructure.csproj", "src/Infrastructure/CRUDTest.Infrastructure/"]
COPY ["src/Infrastructure/CRUDTest.Persistense/CRUDTest.Persistense.csproj", "src/Infrastructure/CRUDTest.Persistense/"]
RUN dotnet restore "src/Presentation/CRUDTest.Presentation/CRUDTest.Presentation.csproj"
COPY . .
WORKDIR "/src/src/Presentation/CRUDTest.Presentation"
RUN dotnet build "CRUDTest.Presentation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CRUDTest.Presentation.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CRUDTest.Presentation.dll"]