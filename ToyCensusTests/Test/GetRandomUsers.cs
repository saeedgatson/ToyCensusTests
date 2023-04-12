using ToyCensusTests.Model;
using ToyCensusTests.Utilites;

namespace ToyCensusTests.Test;

[TestFixture]
public class GetRandomUsers
{
    private RandomUserClient userClient;

    [SetUp]
    public void Setup()
    {
         userClient = new RandomUserClient();
    }

    [Test]
    public void GetUserFromRandomAPI()
    {
        //Act
        var user = userClient.GetUser();

        //Assert
        Assert.That(user.Name.First, Is.EqualTo("Jacinto"));
        Assert.That(user.Gender, Is.EqualTo("male"));
        Assert.That(user.Nat, Is.EqualTo("MX"));
    }

    [Test]
    public void GetUsersFromRandomAPIWithParams()
    {
        //Act
        RandomUserParams randomUserParams = new RandomUserParams(2, "male", "MX", "lower","saeed");
        var users = userClient.GetUsers(randomUserParams);

        //Asserts
        Assert.That(users[0].Name.First, Is.EqualTo("Jacinto"));
        Assert.That(users[0].Gender, Is.EqualTo("male"));
        Assert.That(users[0].Nat, Is.EqualTo("MX"));
        Assert.That(users[1].Name.First, Is.EqualTo("Gilberto"));
        Assert.That(users[1].Gender, Is.EqualTo("male"));
        Assert.That(users[1].Nat, Is.EqualTo("MX"));
    }

    [Test]
    public void GetMaxRandomUsers()
    {
        //Act
        RandomUserParams randomUserParams = new RandomUserParams(5000, null, "US", null, null);
        var users = userClient.GetUsers(randomUserParams);

        //Asserts
        Assert.That(users.Count, Is.EqualTo(5000));
    }

    [TearDown]
    public void Close()
    {
        Console.WriteLine("Hey! I' done executing all the tests");
    }
}