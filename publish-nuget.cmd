del *.nupkg
.\.nuget\nuget pack -Properties Configuration=Release && .\.nuget\nuget push -Source nuget.org *.nupkg 