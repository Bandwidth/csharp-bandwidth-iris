@echo off

del *.nupkg

SET /p apiKey=Input the Nuget API Key:  
.\.nuget\nuget pack -Properties Configuration=Release && dotnet nuget push Bandwidth.Iris.1.0.11.nupkg -s nuget.org -k %apiKey%

