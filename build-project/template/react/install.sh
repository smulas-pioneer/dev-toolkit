#!/usr/bin/env bash

cd $(dirname $0)

docker rmi dev-toolkit-build-project-react
docker build -t dev-toolkit-build-project-react .

