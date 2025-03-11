# # Используем базовый образ .NET SDK для сборки
# FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# # Устанавливаем рабочую директорию
# WORKDIR /src

# # Копируем файлы проекта
# COPY ./Library.Api/*.csproj ./Library/Library.Api/
# COPY ./Library.Application/*.csproj ./Library/Library.Application/
# COPY ./Library.Infrastructure/*.csproj ./Library/Library.Infrastructure/
# COPY ./Library.Core/*.csproj ./Library/Library.Core/
# COPY ./Library.Test/*.csproj ./Library/Library.Test/

# # Восстанавливаем зависимости для всех проектов
# RUN dotnet restore ./Library/Library.Api/Library.Api.csproj

# # Копируем все файлы приложения
# COPY . ./Library

# # Собираем проект
# RUN dotnet build ./Library/Library.Api/Library.Api.csproj -c Release -o /app/build

# # Создаем финальный образ
# FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

# # Копируем собранное приложение в финальный образ
# WORKDIR /app
# COPY --from=build /app/build .

# # Открываем порт
# EXPOSE 80

# # Запускаем приложение
# ENTRYPOINT ["dotnet", "Library.Api.dll"]

# Используем базовый образ .NET SDK для сборки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Устанавливаем рабочую директорию
WORKDIR /src

# Копируем файлы проекта
COPY ./Library.Api/*.csproj ./Library.Api/
COPY ./Library.Application/*.csproj ./Library.Application/
COPY ./Library.Infrastructure/*.csproj ./Library.Infrastructure/
COPY ./Library.Core/*.csproj ./Library.Core/
COPY ./Library.Test/*.csproj ./Library.Test/

# Восстанавливаем зависимости для всех проектов
RUN dotnet restore ./Library.Api/Library.Api.csproj

# Копируем все файлы приложения
COPY . .

# Собираем проект
RUN dotnet build ./Library.Api/Library.Api.csproj -c Release -o /app/build

# Создаем финальный образ
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

# Копируем собранное приложение в финальный образ
WORKDIR /app
COPY --from=build /app/build .

# Открываем порт
EXPOSE 80

# Запускаем приложение
ENTRYPOINT ["dotnet", "Library.Api.dll"]