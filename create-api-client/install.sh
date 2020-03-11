#!/usr/bin/env bash

cd $(dirname $0)

if [[ "$(docker images -q dev-toolkit-create-api-client 2> /dev/null)" != "" ]]; then
  docker rmi dev-toolkit-create-api-client > /dev/null
fi

docker build --quiet -t dev-toolkit-create-api-client . > /dev/null

#cp ./bin/* /usr/local/bin
