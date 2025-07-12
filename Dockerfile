FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . .
WORKDIR /src/MPago.API

RUN dotnet restore MPago.API.sln

RUN dotnet publish MPago.API.sln -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /out .

EXPOSE 80
ENTRYPOINT ["dotnet", "MPago.API.dll"]
