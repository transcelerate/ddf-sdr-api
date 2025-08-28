# ------------ Build stage ------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and project files
COPY src/TransCelerate.SDR.sln ./
COPY src/TransCelerate.SDR.WebApi/*.csproj TransCelerate.SDR.WebApi/
COPY src/TransCelerate.SDR.Core/*.csproj TransCelerate.SDR.Core/
COPY src/TransCelerate.SDR.DataAccess/*.csproj TransCelerate.SDR.DataAccess/
COPY src/TransCelerate.SDR.Service/*.csproj TransCelerate.SDR.Service/
COPY src/TransCelerate.SDR.RuleEngine/*.csproj TransCelerate.SDR.RuleEngine/

# Restore Api project
RUN dotnet restore TransCelerate.SDR.WebApi/TransCelerate.SDR.WebApi.csproj

# Copy source code
COPY src/TransCelerate.SDR.WebApi/ TransCelerate.SDR.WebApi/
COPY src/TransCelerate.SDR.Core/ TransCelerate.SDR.Core/
COPY src/TransCelerate.SDR.DataAccess/ TransCelerate.SDR.DataAccess/
COPY src/TransCelerate.SDR.Service/ TransCelerate.SDR.Service/
COPY src/TransCelerate.SDR.RuleEngine/ TransCelerate.SDR.RuleEngine/

# Publish project
RUN dotnet publish TransCelerate.SDR.WebApi/TransCelerate.SDR.WebApi.csproj -c Release -o /app/publish \
    --self-contained true -r linux-x64 \
    /p:PublishSingleFile=true /p:IncludeNativeLibrariesForSelfExtract=true

# ------------ Runtime stage ------------
FROM ubuntu:24.04 AS runtime
ENV DEBIAN_FRONTEND=noninteractive \
    DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=1 \
    ASPNETCORE_URLS=http://+:80 \
    CdiscRulesEngine="/app/cdisc-rules-engine" \
    CdiscRulesEngineRelativeBinary="core" \
    CdiscRulesEngineRelativeCache="resources/cache"
WORKDIR /app

# Install dependencies
RUN apt-get update && apt-get install -y \
    curl \
    unzip \
    jq \
    tini \
    python3.12 \
    python3.12-venv \
    python3-pip \
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
    && mkdir -p cdisc-rules-engine \
    && mv core-ubuntu-latest/core/* cdisc-rules-engine/ \
    && rm -rf core-ubuntu-latest \
    && chmod +x /app/cdisc-rules-engine/$CdiscRulesEngineRelativeBinary \
    && mkdir -p /app/cdisc-rules-engine/$CdiscRulesEngineRelativeCache

# Copy published files from build stage
COPY --from=build /app/publish ./

# Set permissions
RUN mkdir -p /app /tmp

# Expose port
EXPOSE 80

ENTRYPOINT [ "/usr/bin/tini", "--", "./TransCelerate.SDR.WebApi" ]