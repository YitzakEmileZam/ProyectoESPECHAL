# Etapa 1: Construcción
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copiar el archivo del proyecto y restaurar dependencias
COPY Pg1/Pg1.csproj ./Pg1/
WORKDIR /app/Pg1
RUN dotnet restore

# Copiar el resto de los archivos y compilar la aplicación
COPY Pg1/. ./
RUN dotnet publish -c Release -o out

# Etapa 2: Imagen final
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copiar los archivos publicados desde la etapa de construcción
COPY --from=build-env /app/Pg1/out .

# Configurar variables de entorno necesarias para Render
ENV ASPNETCORE_URLS=http://+:8080
ENV DOTNET_RUNNING_IN_CONTAINER=true
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

# Exponer el puerto que Render utilizará
EXPOSE 8080

# Comando para ejecutar la aplicación
ENTRYPOINT ["dotnet", "Pg1.dll"]