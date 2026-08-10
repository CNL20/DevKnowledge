# Khung Dockerfile cho DevKnowledge.API - hoàn thiện multi-stage build ở Foundation Setup thực tế
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
# COPY + dotnet restore/publish -> hoàn thiện khi solution có project files thật

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
# COPY --from=build /app/publish .
# ENTRYPOINT ["dotnet", "DevKnowledge.API.dll"]
