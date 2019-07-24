FROM mono

RUN mkdir -p /app

COPY tpl /app/tpl/
COPY NSwag /app/nswag/
COPY create-client.sh /app/

WORKDIR /app

ENTRYPOINT ["/app/create-client.sh"]
