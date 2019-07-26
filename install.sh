#!/usr/bin/env bash

./sync-toolkit/install.sh
./create-api-client/install.sh
./create-project/install.sh
./build-project/install.sh

chmod +x /usr/local/bin/dt-*

# remove dangling images
docker image prune -f
