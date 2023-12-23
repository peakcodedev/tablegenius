FROM mcr.microsoft.com/dotnet/aspnet:8.0-bookworm-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0-bookworm-slim AS build
WORKDIR /src
COPY TableGenius.Api.Web/TableGenius.Api.Web.csproj TableGenius.Api.Web/
COPY TableGenius.Api.Presentation/TableGenius.Api.Presentation.csproj TableGenius.Api.Presentation/
COPY TableGenius.Api.Entities/TableGenius.Api.Entities.csproj TableGenius.Api.Entities/
COPY TableGenius.Api.Infrastructure/TableGenius.Api.Infrastructure.csproj TableGenius.Api.Infrastructure/
COPY TableGenius.Api.Services/TableGenius.Api.Services.csproj TableGenius.Api.Services/
COPY TableGenius.Api.Repo.Database/TableGenius.Api.Repo.Database.csproj TableGenius.Api.Repo.Database/
COPY TableGenius.Api.Repo.BlobStorage/TableGenius.Api.Repo.BlobStorage.csproj TableGenius.Api.Repo.BlobStorage/
COPY TableGenius.Api.Settings/TableGenius.Api.Settings.csproj TableGenius.Api.Settings/
RUN dotnet restore "TableGenius.Api.Web/TableGenius.Api.Web.csproj"
COPY . .
WORKDIR "/src/TableGenius.Api.Web"
RUN dotnet build "TableGenius.Api.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TableGenius.Api.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TableGenius.Api.Web.dll"]