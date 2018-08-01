FROM microsoft/aspnetcore:2.0-nanoserver-1709 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0-nanoserver-1709 AS build
WORKDIR /src
COPY CoreWeb1/CoreWeb1.csproj CoreWeb1/
RUN dotnet restore CoreWeb1/CoreWeb1.csproj
COPY . .
WORKDIR /src/CoreWeb1
RUN dotnet build CoreWeb1.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish CoreWeb1.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "CoreWeb1.dll"]
