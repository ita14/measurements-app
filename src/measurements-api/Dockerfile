FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

RUN  apt-get update \
    && apt-get install -y wget \
    && rm -rf /var/lib/apt/lists/*

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . .

RUN dotnet restore
RUN dotnet test ./tests/Measurements.UnitTests/Measurements.UnitTests.csproj
RUN dotnet publish --no-restore -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=build /app .
COPY ./docker-entrypoint.sh .
RUN chmod +x ./docker-entrypoint.sh

ENV ASPNETCORE_URLS http://*:80

ENTRYPOINT ["./docker-entrypoint.sh"]
