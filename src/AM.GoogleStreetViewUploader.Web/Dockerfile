#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/AM.GoogleStreetViewUploader.Web/AM.GoogleStreetViewUploader.Web.csproj", "src/AM.GoogleStreetViewUploader.Web/"]
RUN dotnet restore "src/AM.GoogleStreetViewUploader.Web/AM.GoogleStreetViewUploader.Web.csproj"
COPY . .
WORKDIR "/src/src/AM.GoogleStreetViewUploader.Web"
RUN dotnet build "AM.GoogleStreetViewUploader.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AM.GoogleStreetViewUploader.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AM.GoogleStreetViewUploader.Web.dll"]