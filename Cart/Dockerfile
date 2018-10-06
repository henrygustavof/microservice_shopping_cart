FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY ["Cart.Api/Cart.Api.csproj", "Cart.Api/"]
COPY ["Cart.Infrastructure/Cart.Infrastructure.csproj", "Cart.Infrastructure/"]
COPY ["Cart.Domain/Cart.Domain.csproj", "Cart.Domain/"]
COPY ["Cart.Application/Cart.Application.csproj", "Cart.Application/"]
RUN dotnet restore "Cart.Api/Cart.Api.csproj"
COPY . .
WORKDIR "/src/Cart.Api"
RUN dotnet build "Cart.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Cart.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Cart.Api.dll"]