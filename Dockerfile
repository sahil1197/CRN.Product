# ===========================
# Build Stage
# ===========================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

# Copy project files first for Docker layer caching
COPY ["CRN.Product.Api/CRN.Product.Api.csproj", "CRN.Product.Api/"]
COPY ["CRN.Product.Application/CRN.Product.Application.csproj", "CRN.Product.Application/"]
COPY ["CRN.Product.Infrastructure/CRN.Product.Infrastructure.csproj", "CRN.Product.Infrastructure/"]

# Restore packages
RUN dotnet restore "CRN.Product.Api/CRN.Product.Api.csproj"

# Copy everything else
COPY . .

WORKDIR "/src/CRN.Product.Api"

# Publish
RUN dotnet publish "CRN.Product.Api.csproj" \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=false

# ===========================
# Runtime Stage
# ===========================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080

EXPOSE 8080

ENTRYPOINT ["dotnet", "CRN.Product.Api.dll"]
