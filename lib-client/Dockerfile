# Используем последнюю версию Node.js
FROM node:20 AS build

# Установите рабочую директорию
WORKDIR /app

# Копируйте package.json и package-lock.json
COPY package*.json ./

# Установите зависимости
RUN npm install

# Копируйте остальные файлы приложения
COPY . .

# Выполните сборку приложения
RUN npm run build
CMD ["npm","run", "preview"]

# # Используйте Nginx для обслуживания статических файлов
# FROM nginx:alpine
# COPY --from=build /app/dist /usr/share/nginx/html
# EXPOSE 80

# # Конфигурация Nginx
# COPY nginx.conf /etc/nginx/conf.d/default.conf
# CMD ["nginx", "-g", "daemon off;"]