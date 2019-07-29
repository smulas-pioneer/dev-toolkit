#!/usr/bin/env bash
echo Build .NETCORE Project

if [ -d "$DT_PROJECT" ]; then
  cd $DT_PROJECT
fi

dotnet publish -c release -o ./build

echo Done!
