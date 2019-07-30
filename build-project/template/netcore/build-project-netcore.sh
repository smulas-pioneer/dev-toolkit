#!/usr/bin/env bash
echo Build .NETCORE Project

no_restore=

if [ -d "$DT_PROJECT" ]; then
  cd $DT_PROJECT

  if [ -f "../dt-build-project-nuget.config" ]; then
    dotnet restore --configfile ../dt-build-project-nuget.config
    no_restore="--no-restore"
  fi

else

  if [ -f "dt-build-project-nuget.config" ]; then
    dotnet restore --configfile dt-build-project-nuget.config
    no_restore="--no-restore"
  fi

fi

dotnet publish -c release $no_restore -o ./build

echo Done!
