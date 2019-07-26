#!/usr/bin/env bash

echo Build .NETCORE Project

dotnet publish -c release -o /build

echo Done!
