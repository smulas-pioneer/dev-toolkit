#!/usr/bin/env bash

./sync-toolkit/install.sh
[ ["$1" -eq "create-api-client"] || ["$1" -eq "all"] ] && ./create-api-client/install.sh
[ ["$1" -eq "create-project"] || ["$1" -eq "all"] ] && ./create-project/install.sh
[ ["$1" -eq "create-build-project"] || ["$1" -eq "all"] ] && ./build-project/install.sh

chmod +x /usr/local/bin/dt-*

# remove dangling images
docker image prune -f  > /dev/null
