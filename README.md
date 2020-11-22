# TodoListMicroService
A small microservice orianted messaging app which is developed in .Net Core 3.1.
- Contains two Web API; one is TodoList API, the other one is User API. Both project architecture is based on Clean Architecture.
- JWT Bearer Token was used for authentication.
- TodoList Project has Unit Test project and use NUnit and Moq
- Both Project use Autofac as IoC


**To run project Docker Desktop must be installed.**
**Go to solution root folder and run following command to start docker compose**
```
docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d
```
**After that, you can try api calls on Swagger pages of APIs**

- TodoList API: [http://localhost:8000/swagger/index.html](http://localhost:8000/swagger/index.html).
- User API: [http://localhost:9000/swagger/index.html](http://localhost:9000/swagger/index.html).

**Also you can examine logs on Seq page**
- Seq Log Page: [http://localhost:5341/#/events](http://localhost:5341/#/events).
