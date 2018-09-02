FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY ["Identity.Api/Identity.Api.csproj", "Identity.Api/"]
COPY ["Identity.Infrastructure/Identity.Infrastructure.csproj", "Identity.Infrastructure/"]
COPY ["Identity.Domain/Identity.Domain.csproj", "Identity.Domain/"]
COPY ["Identity.Application/Identity.Application.csproj", "Identity.Application/"]
RUN dotnet restore "Identity.Api/Identity.Api.csproj"
COPY . .
WORKDIR "/src/Identity.Api"
RUN dotnet build "Identity.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Identity.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Identity.Api.dll"]