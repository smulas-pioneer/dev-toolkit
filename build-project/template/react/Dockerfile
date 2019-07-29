FROM node:10.16.0

RUN mkdir -p /app/project

COPY build-project-react.sh /app/

WORKDIR /app/project

ENTRYPOINT ["/app/build-project-react.sh"]
