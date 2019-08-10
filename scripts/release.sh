#!/bin/bash

set -e

version=$1
rawVersion=$(echo $version | sed "s/v//")

if [ $(cat .version) != "$version" ]; then
    echo "$version" > .version
    git add .version
    git commit -m "$version"
    git tag $version
    git push
    git push --tags
fi

cd src
dotnet build -c Release
dotnet test --framework netcoreapp2.1
dotnet nuget push -k $NUGET_KEY -s api.nuget.org ../bin/Packages/Release/Cythral.CodeGeneration.Roslyn.${rawVersion}.nupkg
dotnet nuget push -k $NUGET_KEY -s api.nuget.org ../bin/Packages/Release/Cythral.CodeGeneration.Roslyn.Engine.${rawVersion}.nupkg