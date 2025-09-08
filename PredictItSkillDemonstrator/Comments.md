# General comments for the PredictIt sample

## Quick Review

Needed to change to the latest version of dotnet from 5, since it has been out of support for a few years now.  
This includes updating included libraries.

1. Create a git repository.
1. Update to .NET 9
    1. Update dependencies.
1. Remove unused ```using``` clauses.
1. Set up ```Directory.Build.props``` to set all project settings for solution.
    > Not particularly useful in a solution with only 1 project.
1. Used file-scoped namespaces.
    > I find that this makes the code a little more readable by removing the extra indention.
1. Added ```this``` keyword.
    > I find it easier for me to know when a property is part of a class or something else.
1. Threw some exceptions so code would compile. 
    1. Added ```TODO``` comments to make sure I follow up.
1. Added comments and initializers to class properties.
    1. Modified some types (DateTime to DateOnly)
    1. Added support to generate Swagger documentation from XMLDOC.
1. Started answering questions.
    1. Question 1: Started 13:25. Ended 13:30
    1. Question 2: Started 13:30. Ended 13:42
        1. Corrected spelling
        1. Changed WeatherHelper to be used via DI as a singleton.
    1. Question 3: Started 13:42. Ended 13:45.
    1. Question 4: Started 13:45. Ended 14:49.
        1. Testing in LinqPad: 14:19
        1. Glad I validated. Completed 14:48