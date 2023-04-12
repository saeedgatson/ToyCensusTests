using ToyCensusTests.Model;
using ToyCensusTests.Utilites;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ToyCensusTests.Test;

[TestFixture]
public class CountByCountry
{
    private ToyCensusClient toyCensusClient;

    [SetUp]
    public void Setup()
    {
        toyCensusClient = new ToyCensusClient();
    }

    [Test]
    public void CountByCountryTypeReturns()
    {
        //Arrange
        toyCensusClient.CreatePostRequest("CountByCountry", 0, new RandomUserParams());

        //Act
        var response = toyCensusClient.SubmitRequest();
        Console.WriteLine("Printing results : " + response.Content);

        //Assert
        var result = JsonSerializer.Deserialize<List<CensusToyResponse>>(response.Content);
        Assert.That(result[0].Name, Is.EqualTo("MX"));
        Assert.That(result[0].Value, Is.EqualTo(1));
    }

    [Test]
    public void MultipleCountriesReturned()
    {
        //Arrange
        RandomUserParams randomUserParams = new RandomUserParams(5, null, null, null, "saeed");
        toyCensusClient.CreatePostRequest("CountByCountry", 0, randomUserParams);

        //Act
        var response = toyCensusClient.SubmitRequest();
        Console.WriteLine("Printing results : " + response.Content);

        //Assert
        var result = JsonSerializer.Deserialize<List<CensusToyResponse>>(response.Content);
        Assert.That(result[0].Name, Is.EqualTo("MX"));
        Assert.That(result[0].Value, Is.EqualTo(2));
        Assert.That(result[1].Name, Is.EqualTo("DK"));
        Assert.That(result[1].Value, Is.EqualTo(1));
        Assert.That(result[2].Name, Is.EqualTo("FI"));
        Assert.That(result[2].Value, Is.EqualTo(1));
        Assert.That(result[3].Name, Is.EqualTo("AU"));
        Assert.That(result[3].Value, Is.EqualTo(1));
    }

    [Test]
    [Ignore("Skipping test due to BUG ID:002")]
    public void TopParamEffectsCountByCountry()
    {
        //Arrange
        int top = 2;
        RandomUserParams randomUserParams = new RandomUserParams(2222, null, "MX,AU,US", null, "foo");
        toyCensusClient.CreatePostRequest("CountByCountry", top, randomUserParams);

        //Act
        var response = toyCensusClient.SubmitRequest();
        Console.WriteLine("Printing results : " + response.Content);

        //Assert
        var result = JsonSerializer.Deserialize<List<CensusToyResponse>>(response.Content);
        Assert.That(result.Count, Is.LessThanOrEqualTo(top)); //TEST FAILURE - BUG ID:002
    }

    [OneTimeTearDown]
    public void Cleanup()
    {
        Console.WriteLine("Hey! I'm done executing all the tests");
    }
}