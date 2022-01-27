FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Management.Services/Management.Services.csproj", "Management.Services/"]
RUN dotnet restore "Management.Services/Management.Services.csproj"
COPY . .
WORKDIR "/src/Management.Services"
RUN dotnet build "Management.Services.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Management.Services.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Management.Services.dll"]
