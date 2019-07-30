#!/usr/bin/env bash

cd $(dirname $0)

if [[ "$(docker images -q dev-toolkit-build-project-netcore 2> /dev/null)" != "" ]]; then
  docker rmi dev-toolkit-build-project-netcore
fi

docker build --quiet -t dev-toolkit-build-project-netcore . > /dev/null
