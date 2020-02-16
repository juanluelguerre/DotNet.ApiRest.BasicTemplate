FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
COPY ["src/ElGuerre.Items.Api/ElGuerre.Items.Api.csproj", "ElGuerre.Items.Api/"]
RUN dotnet restore "ElGuerre.Items.Api/ElGuerre.Items.Api.csproj"
COPY . .
RUN dotnet build "src/ElGuerre.Items.Api/ElGuerre.Items.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "src/ElGuerre.Items.Api/ElGuerre.Items.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ElGuerre.Items.Api.dll"]