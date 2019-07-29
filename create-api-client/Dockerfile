FROM mono

RUN mkdir -p /app

COPY tpl /app/tpl/
COPY NSwag /app/nswag/
COPY create-api-client.sh /app/

WORKDIR /app

ENTRYPOINT ["/app/create-api-client.sh"]
