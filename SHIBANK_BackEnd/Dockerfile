# Etapa 1: Utiliza la imagen base de .NET 7 para establecer la etapa "base"
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

# Etapa 2: Establece la etapa "build"
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /app

# Copia el archivo de proyecto
COPY ["SHIBANK.csproj", "./"]

# Restaura las dependencias
RUN dotnet restore

# Copia todo el contenido del proyecto
COPY . .

# Compila la aplicación
RUN dotnet build -c Release -o /app/build

# Publica la aplicación
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

# Etapa 3: Configura la imagen final
FROM base AS final
COPY --from=publish /app/publish .
EXPOSE 7150
ENV ASPNETCORE_URLS=http://0.0.0.0:7150   
ENTRYPOINT ["dotnet", "SHIBANK.dll"]


#docker build -t shibank_backend .
#docker run -d -p 7150:7150 shibank_backend