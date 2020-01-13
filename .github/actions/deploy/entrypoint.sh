#!/bin/sh

ls -la

dotnet build --configuration Release
NU_PATH="$(find . -name *.nupkg)"

dotnet nuget push $NU_PATH -s nuget.org -k $NUGET_KEY
