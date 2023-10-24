FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app
COPY ["SHIBANK.csproj", "./"]
RUN dotnet restore
COPY . .
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
COPY --from=publish /app/publish .
EXPOSE 7150

#Configure for bd
ENV ASPNETCORE_URLS=https://0.0.0.0:7150   
ENV ConnectionStrings__DefaultConnection "Server=host.docker.internal,62637;Database=SHIBANKDB;User Id=sa;Password=290896;Encrypt=False"

#Configure for https certificate
COPY certificate.pfx /app/certificate.pfx
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/app/certificate.pfx
ENV ASPNETCORE_Kestrel__Certificates__Default__Password=punpun

ENTRYPOINT ["dotnet", "SHIBANK.dll"]

#docker build -t shibank_backend .
#docker run -p 7150:7150 -d shibank_backend