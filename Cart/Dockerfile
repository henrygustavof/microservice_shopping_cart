FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY ["Product.Api/Product.Api.csproj", "Product.Api/"]
COPY ["Product.Infrastructure/Product.Infrastructure.csproj", "Product.Infrastructure/"]
COPY ["Product.Domain/Product.Domain.csproj", "Product.Domain/"]
COPY ["Product.Application/Product.Application.csproj", "Product.Application/"]
RUN dotnet restore "Product.Api/Product.Api.csproj"
COPY . .
WORKDIR "/src/Product.Api"
RUN dotnet build "Product.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Product.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Product.Api.dll"]