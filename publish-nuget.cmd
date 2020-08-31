@echo off

del *.nupkg

dotnet build --configuration Release

SET /p apiKey=Input the Nuget API Key:  
.\.nuget\nuget pack -Properties Configuration=Release && dotnet nuget push Bandwidth.Iris.*.nupkg -s nuget.org -k %apiKey%

