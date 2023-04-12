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

# Bugs Found

## ID: 002

**Title/Name :** Top Parameter Doesn’t Limit Returned Results

**Summary :** When the “top” parameter is non-null and greater than zero it should be the maximum number of results returned.

**Environment :** PROD

**Severity :** P0

**Steps to reproduce :** 

1. Create a POST body that has multiple users from 3 different countries.
2. Set the actionType to `CountByCountry` , and the top to `2` .
3. Submit a Post Request to endpoint and view the results.

**Expected vs. actual results :** Expected - 2 or less results are returned. Actual - 3 results are being returned `Printing results : [{"name":"US","value":747},{"name":"MX","value":786},{"name":"AU","value":689}]`

## ID: 001

**Title/Name :** No Error Message Returned When Invlaid Type Is Used

**Summary :** An empty string is returns when sending an HTTP POST request to the Toy Census API with and invalid “actionType”.

**Environment :** PROD

**Severity :** P1

**Console logs :**

```bash
Response Headers
Content-Type: application/json
Content-Length: 0
Connection: keep-alive
Date: Wed, 12 Apr 2023 07:51:39 GMT
x-amzn-RequestId: ee2dc0ac-244b-4388-915c-522324c14c14
x-amz-apigw-id: DQRp4E3CIAMFRww=
X-Amzn-Trace-Id: Root=1-6436630b-363a6d612d995573060e9aa3;Sampled=0;lineage=6e69f56a:0
X-Cache: Miss from cloudfront
Via: 1.1 ec18462cf9d88c8bdb0cd5e50dbe442a.cloudfront.net (CloudFront)
X-Amz-Cf-Pop: IAD89-P2
X-Amz-Cf-Id: a9lJL5GKm8c3SrS-IadPveXP7cmjTy6PWGxfs50uMS0GvqHrM7ubgw==
Response Body:
```

**Steps to reproduce :** Submit a Post Request to endpoint with the following JSON body. See that the “actionType” is set to “CountCountry” which is invalid.

```json
{
  "actionType": "CountCountry",
  "top": 0,
  "users": [
    {
      "gender": "female",
      "name": {
        "title": "miss",
        "first": "lorraine",
        "last": "bryant"
      },
      "location": {
        "street": "4422 harrison ct",
        "city": "gladstone",
        "state": "tasmania",
        "postcode": 3294
      },
      "email": "lorraine.bryant@example.com",
      "login": {
        "username": "smallduck444",
        "password": "aaron",
        "salt": "fYBp4g4a",
        "md5": "5d0785427febd6d262f00929c10247e7",
        "sha1": "a12a6925740eefebebcbc79add949fa108a78bf0",
        "sha256": "61a79aebd11cd5910dd9e77dd49b3dd11df2b4b397e328697f33b86e5b082b84"
      },
      "dob": "1956-09-17 02:13:36",
      "registered": "2009-05-03 14:40:51",
      "phone": "00-3540-6154",
      "cell": "0498-678-691",
      "id": {
        "name": "TFN",
        "value": "377488473"
      },
      "picture": {
        "large": "https://randomuser.me/api/portraits/women/38.jpg",
        "medium": "https://randomuser.me/api/portraits/med/women/38.jpg",
        "thumbnail": "https://randomuser.me/api/portraits/thumb/women/38.jpg"
      },
      "nat": "AU"
    }
  ]
}
```

**Expected vs. actual results :** Expected - Error message is returned. Actual - Empty string is returned.