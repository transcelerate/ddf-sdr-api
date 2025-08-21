# ------------ Build stage ------------
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

# ------------ Runtime stage ------------
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app

# Install dependencies
RUN apt-get update && apt-get install -y \
    software-properties-common \
    curl \
    unzip \
    jq \
    tini \
    && add-apt-repository ppa:deadsnakes/ppa -y \
    && apt-get update && apt-get install -y \
    python3.12 \
    python3.12-distutils \
    && rm -rf /var/lib/apt/lists/*

# Download CDISC Rules Engine
RUN LATEST_RELEASE_URL=$(curl -s --fail --retry 3 https://api.github.com/repos/cdisc-org/cdisc-rules-engine/releases/latest | jq -r '.assets[] | select(.name == "core-ubuntu-latest.zip") | .browser_download_url') \
    && echo "Downloading from: $LATEST_RELEASE_URL" \
    && curl -L \
        --fail \
        --retry 10 \
        --retry-delay 5 \
        --retry-max-time 1800 \
        --retry-connrefused \
        --retry-all-errors \
        --connect-timeout 30 \
        -C - \
        -o core-ubuntu-latest.zip \
        "$LATEST_RELEASE_URL" \
    && unzip core-ubuntu-latest.zip -d core-ubuntu-latest \
    && rm core-ubuntu-latest.zip \
    && mkdir cdisc-rules-engine \
    && mv core-ubuntu-latest/core/* cdisc-rules-engine/ \
    && rm -rf core-ubuntu-latest \
    && chmod +x /app/cdisc-rules-engine/core

ENV CdiscRulesEngine="/app/cdisc-rules-engine/core"

# Copy published files from build stage
COPY --from=build /app/publish .

# Create a non-root user
RUN useradd -m apiuser

# Set permissions
RUN chown -R apiuser:apiuser /app

# Switch to non-root user
USER apiuser

# Set environment variables
ENV ASPNETCORE_URLS=http://+:80

# Expose port
EXPOSE 80

ENTRYPOINT [ "/usr/bin/tini", "--" ]
CMD ["dotnet", "TransCelerate.SDR.WebApi.dll"]