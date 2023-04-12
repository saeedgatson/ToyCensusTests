using ToyCensusTests.Model;
using ToyCensusTests.Utilites;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ToyCensusTests.Test;

[TestFixture]
public class CountPasswordComplexity
{
    private ToyCensusClient toyCensusClient;

    [SetUp]
    public void Setup()
    {
        toyCensusClient = new ToyCensusClient();
    }

    [Test]
    public void CountPasswordComplexityReturns()
    {
        //Arrange
        toyCensusClient.CreatePostRequest("CountPasswordComplexity", 0);

        //Act
        var response = toyCensusClient.SubmitRequest();
        Console.WriteLine("Printing results : " + response.Content);

        //Assert
        var result = JsonSerializer.Deserialize<List<CensusToyResponse>>(response.Content);
        Assert.That(result[0].Name, Is.EqualTo("talon"));
        Assert.That(result[0].Value, Is.EqualTo(0));
    }

    [Test]
    public void MultipleComplexPasswordsReturned()
    {
        //Arrange
        RandomUserParams randomUserParams = new RandomUserParams(22, null, "MX,AU,US", "special,upper,lower,number", "foo");
        toyCensusClient.CreatePostRequest("CountPasswordComplexity", 0, randomUserParams);

        //Act
        var response = toyCensusClient.SubmitRequest();
        Console.WriteLine("Printing results : " + response.Content);

        //Assert
        var result = JsonSerializer.Deserialize<List<CensusToyResponse>>(response.Content);
        Assert.That(result[0].Value, Is.EqualTo(31));
        Assert.That(result[1].Value, Is.EqualTo(30));
        Assert.That(result[2].Value, Is.EqualTo(28));
        Assert.That(result[3].Value, Is.EqualTo(25));
    }

    [Test]
    public void ComplexValueMatchesSpecialCharecters()
    {
        //Arrange
        RandomUserParams randomUserParams = new RandomUserParams(1, null, "US", "special,32", "foo");
        toyCensusClient.CreatePostRequest("CountPasswordComplexity", 0, randomUserParams);

        //Act
        var response = toyCensusClient.SubmitRequest();
        Console.WriteLine("Printing results : " + response.Content);

        //Assert
        var result = JsonSerializer.Deserialize<List<CensusToyResponse>>(response.Content);
        Assert.That(result[0].Name.Length, Is.EqualTo(result[0].Value));
    }

    [Test]
    public void TopLimitsCountPasswordComplexityReturn()
    {
        //Arrange
        RandomUserParams randomUserParams = new RandomUserParams(22, null, "MX,AU,US", "special,upper,lower,number", "foo");
        int top = 2;
        toyCensusClient.CreatePostRequest("CountPasswordComplexity", top, randomUserParams);

        //Act
        var response = toyCensusClient.SubmitRequest();
        Console.WriteLine("Printing results : " + response.Content);

        //Assert
        var result = JsonSerializer.Deserialize<List<CensusToyResponse>>(response.Content);
        Assert.That(result.Count, Is.LessThanOrEqualTo(top));
    }

    [OneTimeTearDown]
    public void Cleanup()
    {
        Console.WriteLine("Hey! I'm done executing all the tests");
    }
}