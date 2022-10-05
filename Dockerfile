
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

COPY Lapka.Messages.Api/Lapka.Messages.Api.csproj Lapka.Messages.Api/Lapka.Messages.Api.csproj
COPY Lapka.Messages.Application/Lapka.Messages.Application.csproj Lapka.Messages.Application/Lapka.Messages.Application.csproj
COPY Lapka.Messages.Core/Lapka.Messages.Core.csproj Lapka.Messages.Core/Lapka.Messages.Core.csproj
COPY Lapka.Messages.Infrastructure/Lapka.Messages.Infrastructure.csproj Lapka.Messages.Infrastructure/Lapka.Messages.Infrastructure.csproj
COPY Lapka.Messages.Api/rsa-public-key.pem Lapka.Messages.Api/rsa-public-key.pem
RUN dotnet restore Lapka.Messages.Api

COPY . .
RUN dotnet publish Lapka.Messages.Api -c release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app/out .

ARG BUILD_VERSION
ENV BUILD_VERSION $BUILD_VERSION

ENV ASPNETCORE_URLS=http://+:5050

ENTRYPOINT ["dotnet", "Lapka.Messages.Api.dll"]