# -----------------------------
# Build Stage
# -----------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY . .

RUN dotnet restore "CRN.Product.Api/CRN.Product.Api.csproj"

RUN dotnet publish "CRN.Product.Api/CRN.Product.Api.csproj" \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=false

# -----------------------------
# Runtime Stage
# -----------------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app

EXPOSE 8080

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet","CRN.Product.Api.dll"]