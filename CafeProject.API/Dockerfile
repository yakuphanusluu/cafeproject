FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
# Aynı klasörde olduğumuz için yollar daha kısa
COPY ["CafeProject.API.csproj", "./"]
RUN dotnet restore "CafeProject.API.csproj"
COPY . .
RUN dotnet build "CafeProject.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CafeProject.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=publish /app/publish .
# Veritabanını yanına almayı unutma
COPY cafe.db . 

ENTRYPOINT ["dotnet", "CafeProject.API.dll"]
