FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5029
EXPOSE 7047

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release

WORKDIR /src

# Copy csproj and restore as distinct layers
COPY ["PixelStore.Application/PixelStore.Application.csproj", "PixelStore.Application/"]
COPY ["PixelStore.Domain/PixelStore.Domain.csproj", "PixelStore.Domain/"]
COPY ["PixelStore.Infrastructure/PixelStore.Infrastructure.csproj", "PixelStore.Infrastructure/"]
COPY ["PixelStore.WebApi/PixelStore.WebApi.csproj", "PixelStore.WebApi/"]
RUN dotnet restore "PixelStore.WebApi/PixelStore.WebApi.csproj"

# Copy everything else and build
COPY ["PixelStore.Application/", "PixelStore.Application/"]
COPY ["PixelStore.Domain/", "PixelStore.Domain/"]
COPY ["PixelStore.Infrastructure/", "PixelStore.Infrastructure/"]
COPY ["PixelStore.WebApi/", "PixelStore.WebApi/"]
RUN dotnet build "PixelStore.WebApi/PixelStore.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the application
FROM build AS publish
ARG BUILD_CONFIGURATION=Release

# Notes:
# - The 'crypticpassword' should be a password of your choice.
# - The 'aspnetapp.pfx' is the file name for the exported certificate.
ARG PATH_CERTIFICATES=/.aspnet/https/aspnetapp.pfx
ARG PASS_FOR_CERTIFICATES=crypticpassword

RUN dotnet publish "PixelStore.WebApi/PixelStore.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final stage/image that will be used to run the application
FROM build AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Setup and trust the development certificate
ENV ASPNETCORE_ENVIRONMENT=Development
ENV DOTNET_RUNNING_IN_CONTAINER=true
#ENV ASPNETCORE_URLS=https://+:7047;http://+:5029

# Generate the development certificate
RUN dotnet dev-certs https -ep /.aspnet/https/aspnetapp.pfx -p crypticpassword
RUN dotnet dev-certs https --trust

# Use the development certificate for Kestrel
ENV Kestrel__Certificates__Default__Password="crypticpassword"
ENV Kestrel__Certificates__Default__Path=/.aspnet/https/aspnetapp.pfx

ENTRYPOINT ["dotnet", "PixelStore.WebApi.dll"]
