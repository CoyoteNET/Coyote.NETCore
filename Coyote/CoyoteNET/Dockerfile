FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Coyote/CoyoteNET/Coyote.NETCore.csproj", "CoyoteNET/"]
COPY ["Coyote/CoyoteNET.Application/Coyote.NETCore.Application.csproj", "CoyoteNET/"]
RUN dotnet restore "CoyoteNET/Coyote.NETCore.csproj"
COPY ["Coyote/CoyoteNET.Application/Coyote.NETCore.Application.csproj", "CoyoteNET/"]
RUN dotnet restore "CoyoteNET.Shared/Coyote.NETCore.Application.csproj"
COPY ["Coyote/CoyoteNET.Shared/Coyote.NETCore.Shared.csproj", "CoyoteNET/"]
RUN dotnet restore "CoyoteNET.Shared/Coyote.NETCore.Shared.csproj"
COPY ["Coyote/CoyoteNET.Controllers/Coyote.NETCore.Controllers.csproj", "CoyoteNET/"]
RUN dotnet restore "CoyoteNET.Controllers/Coyote.NETCore.Controllers.csproj"
COPY . .
WORKDIR "/src/CoyoteNET"
RUN dotnet build "Coyote.NETCore.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Coyote.NETCore.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Coyote.NETCore.csproj"]