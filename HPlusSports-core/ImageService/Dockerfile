#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
#EXPOSE 443
EXPOSE 8080


FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["ImageService/ImageService.csproj", "ImageService/"]
RUN dotnet restore "ImageService/ImageService.csproj"
COPY . .
WORKDIR "/src/ImageService"
RUN dotnet build "ImageService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ImageService.csproj" -c Release -o /app/publish

FROM base AS final
ENV ASPNETCORE_URLS=http://+:8080
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ImageService.dll"]