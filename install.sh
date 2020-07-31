#!/usr/bin/env bash

./sync-toolkit/install.sh
[ $1 = "create-api-client" || $1 = "all" ] && ./create-api-client/install.sh
[ $1 = "create-project" || $1 = "all" ] && ./create-project/install.sh
[ $1 = "create-build-project" || $1 = "all" ] && ./build-project/install.sh

chmod +x /usr/local/bin/dt-*

# remove dangling images
docker image prune -f  > /dev/null
