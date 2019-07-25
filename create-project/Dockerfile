FROM centos

RUN mkdir -p /app

COPY template /app/template/
COPY create-project.sh /app/

WORKDIR /app

ENTRYPOINT ["/app/create-project.sh"]
