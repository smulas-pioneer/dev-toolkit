#!/usr/bin/env bash

echo Build React Project

npm config set proxy $http_proxy
npm config set https-proxy $http_proxy
npm config set strict-ssl false

yarn

yarn build

echo Done!
