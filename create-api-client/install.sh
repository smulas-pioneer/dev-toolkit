#!/usr/bin/env bash

cd $(dirname $0)

docker rmi dev-toolkit-create-api-client

docker build -t dev-toolkit-create-api-client .

cp ./bin/* /usr/local/bin
