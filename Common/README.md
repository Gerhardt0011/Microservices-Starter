
## Create and publish package
```powershell
$version="1.0.0"
$owner="[OWNER_HERE]"
$gh_pat="[PAT_HERE]"

dotnet pack Common\ --configuration Release -p:PackageVersion=$version -p:RepositoryUrl=https://github.com/$owner/Common -o ..\packages

dotnet nuget push ..\packages\Common.$version.nupkg --api-key $gh_pat --source github 
```
