﻿# StrongHeart.Features
## Why StrongHeart.Features?
Provides at structured and streamlined way of organizing .NET applications / webapis / console applications ect. 
In this way it is very easy to focus on the business and less on the technical details arround it via decorators to handle all cross cutting concerns.
The framework is covered by unittests.
```c#
public void StartUp()
{
    Assert.Equal(expectedNumber, result);
}
```
It is build on principles like
- imutablilty
- CQS
- SOLID

Features guides the strukturen but is unaware of the implementation details

## Create a feature
### Create a base class for Query and Command like this. This 

## Use a feature
It is easy to use a feature. Just follow below steps:

#### One time only:
1. Add below to your services configuration (eg. Startup.cs for web applications)
```c#
services.AddFeatures(x => //Extension method from namespace StrongHeart.Features.DependencyInjection
{
    x.AddPipelineExtensions(GetPipeLineExtensions());
}, typeof(NameOfAFeatureType).Assembly);
```
2. Add below method
```c#
public IEnumerable<IPipelineExtension> GetPipeLineExtensions()
{
    yield return new AuthorizationExtension();
    //yield more results to extend your pipeline
}
```
3. Create a caller to get started (you can make it more grannular when you are confortable). The caller may be a physical person or a system or similar.
```c#
public class MyCaller : ICaller
{
    public Guid Id { get; } = new Guid("ae463a0e-60ff-429f-a1f5-decd36f17e1d");

    public IReadOnlyList<Claim> Claims { get; } = new List<Claim>()
    {
        StrongHeart.Features.Core.AdminClaim.Instance
    }.AsReadOnly();
}
```

#### Every time:
- Add your feature as dependency with this syntax
```c#
public class MyClass
{
    private readonly IQueryFeature<MyQueryFeatureRequest, MyQueryFeatureResponse> _feature;
    public MyClass(IQueryFeature<MyQueryFeatureRequest, MyQueryFeatureResponse> feature)
    {
        _feature = feature;
    }
}
```
or with ASP NET Core Webapi 
```c#
public async IActionResult GetSomeThing([FromServices] IQueryFeature<MyQueryFeatureRequest, MyQueryFeatureResponse> feature)
{
    MyQueryFeatureResponse result = await feature.Execute(new MyQueryFeatureRequest(new MyCaller(), /*other arguments*/));
    /*return proper data*/
}

```

## Add out-of-the-box cross cutting concerns to features (pipeline)
|                                        Pipeline extension                                       |                   Setup                   | Interface to implement on your feature |
|-----------------------------------------------------------------------------------------------|-----------------------------------------|----------------------------------------|
| Authorization: Ensures that the caller has sufficient permissions to execute a feature | yield return new AuthorizationExtension() | IAuthorizable                          |
| ExceptionLogging: will log all exceptions which a feature may throw                                         | yield return new ExceptionLoggerExtension()             | n/a
| Filtering: will filter data in the response dependent on who calls                                        | yield return ? |IFilterable'TResponse'
| RequestValidation: Will validate every request in a streamlines way                                              |                                          | IRequestValidatable


## Create your own pipeline extension
There are many candidates for custom pipeline extension. Just to name a few:
- Transaction (ensures that the subtasks in a command executes in a transaction)
- Audit (ensures that all calls to a feature is logged for audit purposes. Usually only relevant on commands, but might also be interesting on queries)
- Timealert (ensures to notify 'someone' if a command or query execution time exeeds more than allowed)
- Cache (ensures to cache return values on queries)

## FAQ
- Why not middleware?


# StrongHeart.Core 
## (Feature toggle/idprovider)
# StrongHeart.TestTools.ComponentAnalysis
# StrongHeart.Migrations
# StrongHeart.EfCore