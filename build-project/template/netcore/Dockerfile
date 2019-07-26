FROM microsoft/dotnet

RUN mkdir -p /app/project

COPY build-project-netcore.sh /app/

WORKDIR /app/project

ENTRYPOINT ["/app/build-project-netcore.sh"]
