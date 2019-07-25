#!/usr/bin/env bash

./create-api-client/install.sh

chmod +x /usr/local/bin/dt-*

# remove dangling images
docker image prune -f
