# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Postech.Fase3.Contatos.Update.Service/Postech.Fase3.Contatos.Update.Service.csproj", "Postech.Fase3.Contatos.Update.Service/"]
COPY ["Postech.Fase3.Contatos.Update.Infra/Postech.Fase3.Contatos.Update.Infra.csproj", "Postech.Fase3.Contatos.Update.Infra/"]
COPY ["Postech.Fase3.Contatos.Update.Domain/Postech.Fase3.Contatos.Update.Domain.csproj", "Postech.Fase3.Contatos.Update.Domain/"]
COPY ["Postech.Fase3.Contatos.Update.Infra.CrossCuting/Postech.Fase3.Contatos.Update.Infra.CrossCuting.csproj", "Postech.Fase3.Contatos.Update.Infra.CrossCuting/"]
COPY ["Postech.Fase3.Contatos.Update.Infra.Ioc/Postech.Fase3.Contatos.Update.Infra.Ioc.csproj", "Postech.Fase3.Contatos.Update.Infra.Ioc/"]
COPY ["Postech.Fase3.Contatos.Update.Application/Postech.Fase3.Contatos.Update.Application.csproj", "Postech.Fase3.Contatos.Update.Application/"]
RUN dotnet restore "./Postech.Fase3.Contatos.Update.Service/Postech.Fase3.Contatos.Update.Service.csproj"
COPY . .
WORKDIR "/src/Postech.Fase3.Contatos.Update.Service"
RUN dotnet build "./Postech.Fase3.Contatos.Update.Service.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Postech.Fase3.Contatos.Update.Service.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Postech.Fase3.Contatos.Update.Service.dll"]