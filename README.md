# Web-API приложение библиотеки (C# ASP.Net Core)

## **Инструкции к запуску проекта EventsApplication:**
1. Для запуска необходим [Docker desktop](https://www.docker.com/products/docker-desktop/) и стабильное интернет подключение
2. Клонируйте репозиторий и перейдите в папку с проектом:
  ```
  git clone https://github.com/martineidger/Library-Web.git
  ```
  ```
  cd Library-Web
  ```
3. Запустите проект: убедитесь, что находитесь в одной папке с файлом docker-compose, откройте терминал (или командную строку) и запустите команду
   ```
   docker-compose up --build
   ```
4. Дождитесь полной загрузки и успешного запуска всех (четырех) контейнеров. После запуска сайт доступен по адресу: [Web-Library](http://localhost:80/)
   
## В приложении присутствуют пользователи по умолчанию
### Admin
**Email**: admin@example.com  
**Password**: Admin123!
### User 
**Email**: user@example.com   
**Password**: User123!
