#!/usr/bin/env bash

cd $(dirname $0)

docker rmi dev-toolkit-create-project

docker build --quiet -t dev-toolkit-create-project .

cp ./bin/* /usr/local/bin
