dotnet restore dotnetcoresdk.csproj
dotnet build dotnetcoresdk.csproj -c:Release
nuget pack Packages.nuspec