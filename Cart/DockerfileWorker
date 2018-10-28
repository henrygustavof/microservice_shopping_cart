FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet AS build
WORKDIR /src
COPY ["Cart.Worker/Cart.Worker.csproj", "Cart.Worker/"]
RUN dotnet restore "Cart.Worker/Cart.Worker.csproj"
COPY . .
WORKDIR "/src/Cart.Worker"
RUN dotnet build "Cart.Worker.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Cart.Worker.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Cart.Worker.dll"]