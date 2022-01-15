FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Identity.Services/Identity.Services.csproj", "Identity.Services/"]
RUN dotnet restore "Identity.Services/Identity.Services.csproj"
COPY . .
WORKDIR "/src/Identity.Services"
RUN dotnet build "Identity.Services.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Identity.Services.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Identity.Services.dll"]
