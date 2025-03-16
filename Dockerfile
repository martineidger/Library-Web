FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
# USER $APP_UID
# ARG APP_UID=1000
# RUN adduser -u $APP_UID appuser
# USER appuser
WORKDIR /app
EXPOSE 8080


# Этот этап используется для сборки проекта службы
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Library.Api/Library.Api.csproj", "Library.Api/"]
COPY ["Library.Application/Library.Application.csproj", "Library.Application/"]
COPY ["Library.Core/Library.Core.csproj", "Library.Core/"]
COPY ["Library.Infrastructure/Library.Infrastructure.csproj", "Library.Infrastructure/"]
RUN dotnet restore "./Library.Api/Library.Api.csproj"
COPY . .
WORKDIR "/src/Library.Api"
RUN dotnet build "./Library.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Этот этап используется для публикации проекта службы, который будет скопирован на последний этап
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Library.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Этот этап используется в рабочей среде или при запуске из VS в обычном режиме (по умолчанию, когда конфигурация отладки не используется)
FROM base AS final
WORKDIR /app

RUN mkdir -p /app/wwwroot/covers
# RUN chown -R appuser:appuser /app/wwwroot/covers
RUN chmod -R 777 /app/wwwroot/covers

# Копируйте опубликованные файлы в рабочую директорию
COPY --from=publish /app/publish .

# Копируйте изображения из папки Covers_def в wwwroot
COPY ./Library.Api/Covers_def /app/wwwroot/covers

ENTRYPOINT ["dotnet", "Library.Api.dll"]