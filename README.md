# ToyCensusTests
A demo project to showcase API testing in C# using RestSharp, and NUnit.

 
### Dev Environment

This project is developed and tested on dotnet 7.0.203 on Windows 10 Home with Visual Studio 2022 as the IDE.

### Quick Overview Of The API

The Census Toy API service is a single endpoint that accepts an HTTP POST request. The JSON body requires an `actionType`, and `users` input. There’s also an optional parameter called `top`.  Based on the inputs an ordered list of key / value pairs will be returned. *There’s no limit on the number of requests you may make, and no authentication is required.*

# Testing

At first I test the service manually using Postman. I created different JSON bodies to validate that the service worked as I expected.

My initial thought was to store these different JSON files in a Test Data folder. But I eventually moved over to using the [Random User API](https://randomuser.me/documentation) to help generate a list of users that could be used in Census Toy API  request.

The benefit is that test data creation was rapidly increased. There’s also no longer the need to maintain JSON files in the code base. One downside though, is there’s now a reliance on a third party API.

The tests directory has different tests:

- GetOperation - These are test that I used to quickly validate that Random User API returns new users as expected.
- CountByCountry.cs - Test cases to validate the County By Country action type.
- CountByGender.cs - Test cases to validate the County By Gender action type.
- CountPasswordComplecity.cs - Test cases to validate the County Password Complecity action type.

### Utilities

This project has two helper classes to assist with writing tests:

- RandomUserClient - Helper for creating GET request get new users from the Random User API.
- ToyCensusClient - Helper for creating / submitting POST request to the Census Toy Service.

## What Was Tested

- CountByGender - will return the count of users by whatever gender strings are in the data.
- Created automated test for the following scenarios:
    - given both genders entered, then counts for both are returned
    - given only male users entered, then only male count returned
    - given only female users entered, then only female count returned
    - given one gender entered more than the other, then the higher count is returned first
- CountByCountry - will return the count of users by whatever country string are in the data.
- Created automated test for the following scenarios:
    - given valid input entered, then country counts & names return as expected.
    - given one country entered more than the other, then the higher count is returned first.
    - given top parameter is set, then it correctly effects returned results.
- CountPasswordComplexity - will return the passwords sorted by complexity. Complexity will be considered the number of non-alphanumeric characters in the password.
    - given valid input entered, then password complexity counts return as expected.
    - given multiple complex passwords entered, then the higher count is returned first.
    - given complex passwords, then complexity value matches special character count.
    - given top parameter is set, then it correctly limits returned results.
- Invalid Action Type

# How To Run The Automated Test

1. Clone this repository.

### Visual Studio

1. Build the project.
2. Then go into TEST > WINDOW > TEST EXPLORER (across the top).
3. Run the test from the Test Explorer Window. *It will tell you what has passed and what has failed.*

### VS Code

To run the test in VS Code it helps to have an extension. I would recommend installing the “[.NET Core Test Explorer](https://marketplace.visualstudio.com/items?itemName=formulahendry.dotnet-test-explorer)”

1. You can build the project from terminal with `dotnet build`
2. Then run test using the new extension from above.

Note that you might also need to set the test project path before test can be found. by select the Settings wheel (Manage) > Extensions > Workspace Settings > Test Project Path

### Console

1. The test can also be ran through the terminal using [nunit3-console](https://docs.nunit.org/articles/nunit/running-tests/Console-Runner.html) runner.