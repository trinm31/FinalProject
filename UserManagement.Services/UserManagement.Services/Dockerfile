FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["UserManagement.Services/UserManagement.Services.csproj", "UserManagement.Services/"]
RUN dotnet restore "UserManagement.Services/UserManagement.Services.csproj"
COPY . .
WORKDIR "/src/UserManagement.Services"
RUN dotnet build "UserManagement.Services.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UserManagement.Services.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserManagement.Services.dll"]
