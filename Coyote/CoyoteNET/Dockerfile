FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["CoyoteNET/CoyoteNET.csproj", "CoyoteNET/"]
COPY ["CoyoteNET.Application/CoyoteNET.Application.csproj", "CoyoteNET.Application/"]
COPY ["CoyoteNET.Shared/CoyoteNET.Shared.csproj", "CoyoteNET.Shared/"]
COPY ["CoyoteNET.DAL/CoyoteNET.DAL.csproj", "CoyoteNET.DAL/"]
RUN dotnet restore "CoyoteNET/CoyoteNET.csproj"
COPY . .
WORKDIR "/src/CoyoteNET"
RUN dotnet build "CoyoteNET.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CoyoteNET.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CoyoteNET.dll"]