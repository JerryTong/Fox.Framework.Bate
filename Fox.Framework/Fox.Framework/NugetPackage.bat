del *.nupkg
nuget pack Fox.Framework.csproj -build
nuget push *.nupkg -Source http://localhost:8088/nuget
pause