# Build stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy project files
COPY src/TransCelerate.SDR.WebApi/*.csproj TransCelerate.SDR.WebApi/
COPY src/TransCelerate.SDR.Core/*.csproj TransCelerate.SDR.Core/
COPY src/TransCelerate.SDR.DataAccess/*.csproj TransCelerate.SDR.DataAccess/
COPY src/TransCelerate.SDR.Service/*.csproj TransCelerate.SDR.Service/
COPY src/TransCelerate.SDR.RuleEngine/*.csproj TransCelerate.SDR.RuleEngine/

# Restore Api project
RUN dotnet restore TransCelerate.SDR.WebApi/*.csproj

# Copy source code
COPY src/TransCelerate.SDR.WebApi/ TransCelerate.SDR.WebApi/
COPY src/TransCelerate.SDR.Core/ TransCelerate.SDR.Core/
COPY src/TransCelerate.SDR.DataAccess/ TransCelerate.SDR.DataAccess/
COPY src/TransCelerate.SDR.Service/ TransCelerate.SDR.Service/
COPY src/TransCelerate.SDR.RuleEngine/ TransCelerate.SDR.RuleEngine/

# Build and publish project
RUN dotnet publish TransCelerate.SDR.WebApi/TransCelerate.SDR.WebApi.csproj -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app

# Create a non-root user
RUN useradd -m apiuser

# Copy published files from build stage
COPY --from=build /app/publish .

# Set permissions
RUN chown -R apiuser:apiuser /app

# Switch to non-root user
USER apiuser

# Set environment variables
ENV ASPNETCORE_URLS=http://+:80

# Expose port
EXPOSE 80

ENTRYPOINT ["dotnet", "TransCelerate.SDR.WebApi.dll"]