FROM nginx AS base
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Frontend/ChessAI.csproj", "Frontend/"]
RUN dotnet restore "Frontend/ChessAI.csproj"

COPY . .
WORKDIR "/src/Frontend"
RUN dotnet build "ChessAI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ChessAI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM  base AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY  Frontend/nginx.conf /etc/nginx/nginx.conf