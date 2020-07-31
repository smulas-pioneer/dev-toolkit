#!/usr/bin/env bash

echo "Installing create-project..."

cd $(dirname $0)

if [[ "$(docker images -q dev-toolkit-create-project 2> /dev/null)" != "" ]]; then
  docker rmi dev-toolkit-create-project > /dev/null
fi

docker build --quiet -t dev-toolkit-create-project . > /dev/null

cp ./bin/* /usr/local/bin
