FROM mono:latest

RUN sed -i 's/deb.debian.org/archive.debian.org/g' /etc/apt/sources.list && \
    sed -i 's/security.debian.org/archive.debian.org/g' /etc/apt/sources.list && \
    sed -i '/buster-updates/d' /etc/apt/sources.list && \
    apt-get update && \
    apt-get install -y mono-xsp4 && \
    rm -rf /var/lib/apt/lists/*

WORKDIR /app
COPY . .

RUN nuget restore packages.config -PackagesDirectory packages && \
    mkdir -p bin && \
    cp packages/MySql.Data.8.0.29/lib/net48/MySql.Data.dll bin/ && \
    cp packages/Ubiety.Dns.Core.2.2.1/lib/netstandard2.0/Ubiety.Dns.Core.dll bin/

COPY entrypoint.sh /entrypoint.sh
RUN chmod +x /entrypoint.sh

EXPOSE 80

ENTRYPOINT ["/entrypoint.sh"]
