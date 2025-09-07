# General comments for the PredictIt sample

## Quick Review

Needed to change to the latest version of dotnet from 5, since it has been out of support for a few years now.  
This includes updating included libraries.

1. Update to .NET 9
    1. Update dependencies.
1. Remove unused ```using``` clauses.
1. Set up ```Directory.Build.props``` to set all project settings for solution.
    > Not particularly useful in a solution with only 1 project.
1. Used file scoped namespaces.
    > I find that this makes the code a little more readable by removing the extra indention.
1. 