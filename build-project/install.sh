#!/usr/bin/env bash

cd $(dirname $0)

./template/react/install.sh
./template/netcore/install.sh

cp ./bin/* /usr/local/bin
