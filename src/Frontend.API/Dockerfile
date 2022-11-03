FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Directory.Build.props", "Directory.Build.props"]
COPY ["Frontend.API/Frontend.API.csproj", "Frontend.API/"]
COPY ["Shared/Shared.csproj", "Shared/"]
RUN dotnet restore "Frontend.API/Frontend.API.csproj"
COPY . .
WORKDIR "/src/Frontend.API"
RUN dotnet build "Frontend.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Frontend.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Frontend.API.dll"]
