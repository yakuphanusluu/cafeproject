# 1. Aşama: Derleme (Build)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Klasör ismini belirterek dosyaları içeri alıyoruz
COPY ["CafeProject.API/CafeProject.API.csproj", "CafeProject.API/"]
RUN dotnet restore "CafeProject.API/CafeProject.API.csproj"

COPY . .
WORKDIR "/src/CafeProject.API"
RUN dotnet build "CafeProject.API.csproj" -c Release -o /app/build

# 2. Aşama: Yayınlama (Publish)
FROM build AS publish
RUN dotnet publish "CafeProject.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# 3. Aşama: Çalıştırma (Final)
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=publish /app/publish .

# Veritabanını da klasörün içinden çekip buraya kopyalıyoruz
COPY CafeProject.API/cafe.db . 

ENTRYPOINT ["dotnet", "CafeProject.API.dll"]
