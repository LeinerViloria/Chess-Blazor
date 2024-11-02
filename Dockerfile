FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5055

ENV ASPNETCORE_URLS=http://+:5055

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["Frontend/ChessAI.csproj", "Frontend/"]
RUN dotnet restore "Frontend/ChessAI.csproj"

COPY . .
WORKDIR "/src/Frontend"
RUN dotnet build "ChessAI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ChessAI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChessAI.dll"]
