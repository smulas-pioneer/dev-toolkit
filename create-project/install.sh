#!/usr/bin/env bash

cd $(dirname $0)


docker build -t dev-toolkit-create-project .

cp ./bin/* /usr/local/bin
