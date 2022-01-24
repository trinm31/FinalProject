FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Email.Services/Email.Services.csproj", "Email.Services/"]
RUN dotnet restore "Email.Services/Email.Services.csproj"
COPY . .
WORKDIR "/src/Email.Services"
RUN dotnet build "Email.Services.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Email.Services.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Email.Services.dll"]
