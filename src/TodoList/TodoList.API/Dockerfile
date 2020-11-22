#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/TodoList/TodoList.API/TodoList.API.csproj", "src/TodoList/TodoList.API/"]
COPY ["src/TodoList/TodoList.Application/TodoList.Application.csproj", "src/TodoList/TodoList.Application/"]
COPY ["src/TodoList/TodoList.Core/TodoList.Core.csproj", "src/TodoList/TodoList.Core/"]
COPY ["src/Common/ResultTypes/ResultTypes.csproj", "src/Common/ResultTypes/"]
RUN dotnet restore "src/TodoList/TodoList.API/TodoList.API.csproj"
COPY . .
WORKDIR "/src/src/TodoList/TodoList.API"
RUN dotnet build "TodoList.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TodoList.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TodoList.API.dll"]
