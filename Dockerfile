FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["FinanceTrackerApp/FinanceTrackerApp.csproj", "FinanceTrackerApp/"]
RUN dotnet restore "FinanceTrackerApp/FinanceTrackerApp.csproj"

COPY . .
WORKDIR "/src/FinanceTrackerApp"
RUN dotnet build "FinanceTrackerApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FinanceTrackerApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FinanceTrackerApp.dll"]
