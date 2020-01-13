FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["ElGuerre.Items/ElGuerre.Items.csproj", "ElGuerre.Items/"]
RUN dotnet restore "ElGuerre.Items/ElGuerre.Items.csproj"
COPY . .
WORKDIR "/src/ElGuerre.Items"
RUN dotnet build "ElGuerre.Items.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ElGuerre.Items.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ElGuerre.Items.dll"]