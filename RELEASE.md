Perform a release with the following commands:

```bash
. ./scripts/release.sh <version>
```

Set the $NUGET_KEY environment variable to an API key for the nuget account you want to publish to.

At least one manual release will have to be performed before setting up CICD, since the infrastructure package will rely on this.
