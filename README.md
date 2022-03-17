# Introduction 
Sample project to demonstrate how to implement basic unit testing and integration testing.

# Basic examples covered

## XUnit and NUnit
Both of these test frameworks are great and quite similar. 
There are few things that are different such as how Setup and TearDowns are handled, and attribute names. 
XUnit has a good description [here](https://xunit.net/docs/comparisons) of the differences between these frameworks:

## Libraries
### AutoFixture
AutoFixture allows for alot of customization and in these examples are used either in tandem with these libraries or encapsulates the use of them by adding these as "Customizations". 
The library is a great tool to speed up the process of unit testing and remove the monotony of creating anonymous variables, and if the need arises can be controlled using Test Data builder.

```c#
var fixture = new AutoFixture();

// Create one object
var number = fixture.Create<int>();
var str = fixture.Create<string>();
var valueObject = fixture.Create<MyValueObject>();

// Create Many
var numbers = fixture.CreateMany<int>(10);

// Using builder to customize some properties, and exclude another one. If not present autofixture will randomly assign values
var myObject = fixture.Build<MyObject>()
                      .With(x => x.MyProperty, "Should Have This Value")
                      .Without(x => x.MyOtherProperty)
                      .Create();
```

### NSubstitute
Mocking library, in this repository the examples with under the AutoFixture folder uses NSubstitute as a customization.
For a more direct NSubstitute implementation please check under the NSubstitute folder. 

### Moq
Mocking Library, also exists as a customization for AutoFixture, although not used in the examples for this repository.

### FluentAssertions
This library makes assertions allows us to more naturally specify the expected outcome of a unit tests.

By default an assertion might look something like:
```c#
var result = sut.DoSomething();

Assert.IsTrue(result);
```
Becomes:
```c#
var result = sut.DoSomething();

result.Should().Be().True();
```

# Recommended reads
To dig deeper into the world of tests see links below.

## Unit Testing
* [GivenWhenThen](https://martinfowler.com/bliki/GivenWhenThen.html)
* [Arrange Act Assert](https://xp123.com/articles/3a-arrange-act-assert/)
* [F.I.R.S.T](https://dzone.com/articles/first-principles-solid-rules-for-tests)
* [Microsoft Best Practices](https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-best-practices)

## Integration Testing
* [Microsoft Docs](https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-6.0)
* [Pluralsight - Integration Testing ASP.NET Core Applications: Best Practices](https://app.pluralsight.com/library/courses/integration-testing-asp-dot-net-core-applications-best-practices)

## Test Driven Development
* [Pluralsight Csharp Test Driven Development](https://app.pluralsight.com/library/courses/csharp-test-driven-development)

## Food for thought
* [TDD Revisited NDC](https://www.youtube.com/watch?v=vOO3hulIcsY&ab_channel=NDCConferences)
* [Kevlin Henney — What we talk about when we talk about unit testing](https://www.youtube.com/watch?v=-WWIeXmm4ec&ab_channel=Heisenbug)
