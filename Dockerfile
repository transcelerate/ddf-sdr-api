FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
EXPOSE 80
# EXPOSE 443
ENV ASPNETCORE_URLS=http://+:80
COPY . .
ENTRYPOINT ["dotnet", "TransCelerate.SDR.WebApi.dll"]