#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/TaskManager.DeveloperEvaluation.WebApi/TaskManager.DeveloperEvaluation.WebApi.csproj", "src/TaskManager.DeveloperEvaluation.WebApi/"]
COPY ["src/TaskManager.DeveloperEvaluation.IoC/TaskManager.DeveloperEvaluation.IoC.csproj", "src/TaskManager.DeveloperEvaluation.IoC/"]
COPY ["src/TaskManager.DeveloperEvaluation.Domain/TaskManager.DeveloperEvaluation.Domain.csproj", "src/TaskManager.DeveloperEvaluation.Domain/"]
COPY ["src/TaskManager.DeveloperEvaluation.Common/TaskManager.DeveloperEvaluation.Common.csproj", "src/TaskManager.DeveloperEvaluation.Common/"]
COPY ["src/TaskManager.DeveloperEvaluation.Application/TaskManager.DeveloperEvaluation.Application.csproj", "src/TaskManager.DeveloperEvaluation.Application/"]
COPY ["src/TaskManager.DeveloperEvaluation.ORM/TaskManager.DeveloperEvaluation.ORM.csproj", "src/TaskManager.DeveloperEvaluation.ORM/"]
RUN dotnet restore "./src/TaskManager.DeveloperEvaluation.WebApi/TaskManager.DeveloperEvaluation.WebApi.csproj"
COPY . .
WORKDIR "/src/src/TaskManager.DeveloperEvaluation.WebApi"
RUN dotnet build "./TaskManager.DeveloperEvaluation.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TaskManager.DeveloperEvaluation.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TaskManager.DeveloperEvaluation.WebApi.dll"]