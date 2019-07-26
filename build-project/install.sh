#!/usr/bin/env bash

cd $(dirname $0)

./template/react/install.sh

cp ./bin/* /usr/local/bin
