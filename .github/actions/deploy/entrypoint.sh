#!/bin/sh

dotnet build --configuration Release
NU_PATH="$(find . -name *.nupkg)"

dotnet --version 
dotnet nuget push $NU_PATH -s nuget.org -k $NUGET_KEY
