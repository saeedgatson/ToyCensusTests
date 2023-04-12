using ToyCensusTests.Model;
using ToyCensusTests.Utilites;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ToyCensusTests.Test;

[TestFixture]
public class CountByGender
{
    private ToyCensusClient toyCensusClient;

    [SetUp]
    public void Setup()
    {
        toyCensusClient = new ToyCensusClient();
    }

    [Test]
    public void BothGendersReturned()
    {
        //Arrange
        RandomUserParams randomUserParams = new RandomUserParams(2, null, "US", null, "saeed");
        toyCensusClient.CreatePostRequest("CountByGender", 0, randomUserParams);

        //Act
        var response = toyCensusClient.SubmitRequest();
        Console.WriteLine("Printing results : " + response.Content);

        //Assert
        var result = JsonSerializer.Deserialize<List<CensusToyResponse>>(response.Content);
        Assert.That(result[0].Name, Is.EqualTo("male"));
        Assert.That(result[0].Value, Is.EqualTo(1));
        Assert.That(result[1].Name, Is.EqualTo("female"));
        Assert.That(result[1].Value, Is.EqualTo(1));
    }

    [Test]
    public void MaleGenderOnlyReturned()
    {
        //Arrange
        RandomUserParams randomUserParams = new RandomUserParams(5,"male","US",null,null);
        toyCensusClient.CreatePostRequest("CountByGender", 0, randomUserParams);

        //Act
        var response = toyCensusClient.SubmitRequest();
        Console.WriteLine("Printing results : " + response.Content);

        //Assert
        var result = JsonSerializer.Deserialize<List<CensusToyResponse>>(response.Content);
        Assert.That(result[0].Name, Is.EqualTo("male"));
        Assert.That(result[0].Value, Is.EqualTo(5));
    }

    [Test]
    public void FemaleGenderOnlyReturned()
    {
        //Arrange
        RandomUserParams randomUserParams = new RandomUserParams(5, "female", "US", null, null);
        toyCensusClient.CreatePostRequest("CountByGender", 0, randomUserParams);

        //Act
        var response = toyCensusClient.SubmitRequest();
        Console.WriteLine("Printing results : " + response.Content);

        //Assert
        var result = JsonSerializer.Deserialize<List<CensusToyResponse>>(response.Content);
        Assert.That(result[0].Name, Is.EqualTo("female"));
        Assert.That(result[0].Value, Is.EqualTo(5));
    }

    [Test]
    public void ErrorOnInvalidType()
    {
        //Arrange
        RandomUserParams randomUserParams = new RandomUserParams(2, null, "US", null, "saeed");
        toyCensusClient.CreatePostRequest("countgender", 0, randomUserParams);

        //Act
        var response = toyCensusClient.SubmitRequest();
        Console.WriteLine("Printing results : " + response.Content);

        //Assert
        Assert.That(response.Content, !Is.EqualTo(String.Empty)); //TEST FAILURE - BUG ID:001
    }

    [Test]
    public void LargerGenderCountReturnedFirst()
    {
        //Arrange
        RandomUserParams randomUserParams = new RandomUserParams(2222, null, "US", null, "foo");
        toyCensusClient.CreatePostRequest("CountByGender", 0, randomUserParams);

        //Act
        var response = toyCensusClient.SubmitRequest();
        Console.WriteLine("Printing results : " + response.Content);

        //Assert
        var result = JsonSerializer.Deserialize<List<CensusToyResponse>>(response.Content);
        Assert.That(result[0].Name, Is.EqualTo("female"));
        Assert.That(result[0].Value, Is.EqualTo(1126));
        Assert.That(result[1].Name, Is.EqualTo("male"));
        Assert.That(result[1].Value, Is.EqualTo(1096));
    }

    [OneTimeTearDown]
    public void Cleanup()
    {
        Console.WriteLine("Hey! I'm done executing all the tests");
    }
}