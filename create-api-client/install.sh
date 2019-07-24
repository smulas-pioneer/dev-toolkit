#!/usr/bin/env bash

cd $(dirname $0)


docker build -t dev-toolkit-create-api-client .

cp ./bin/* /usr/local/bin
