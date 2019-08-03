Perform a release with the following commands:

```bash
MAJ=<major release number here>
MIN=<minor release number here>
PATCH=<patch release here>
CONFIG=<Debug|Release>

git tag v${MAJ}.${MIN}.${PATCH}
git push
git push --tags

cd src
dotnet build -c $CONFIG
dotnet test
dotnet nuget push -k $AZURE_KEY -s api.nuget.org ../bin/Packages/${CONFIG}/Cythral.CodeGeneration.Roslyn.${MAJ}.${MIN}.${PATCH}.nupkg
```

At least one manual release will have to be performed before setting up CICD, since the infrastructure package will rely on this.
