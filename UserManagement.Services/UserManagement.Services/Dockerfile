FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["UserManagement.Services.csproj", "./"]
RUN dotnet restore "UserManagement.Services.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "UserManagement.Services.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UserManagement.Services.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserManagement.Services.dll"]
