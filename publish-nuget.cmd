del *.nupkg
.\.nuget\nuget pack -Properties Configuration=Release && .\.nuget\nuget push  *.nupkg