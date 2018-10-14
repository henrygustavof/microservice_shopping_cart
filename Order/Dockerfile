FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY ["Order.Api/Order.Api.csproj", "Order.Api/"]
RUN dotnet restore "Order.Api/Order.Api.csproj"
COPY . .
WORKDIR "/src/Order.Api"
RUN dotnet build "Order.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Order.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Order.Api.dll"]