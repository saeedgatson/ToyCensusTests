using ToyCensusTests.Model.Random;
using ToyCensusTests.Model;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ToyCensusTests.Utilites;

public class RandomUserClient : RestSharp.RestClient
{
    private RestClient client;
    private RestRequest request;
    private RestResponse response;
    private const string BaseUrl = "https://randomuser.me/api/?&noinfo&exc=registered";

    public RandomUserClient()
    {
        this.client = new RestClient(BaseUrl);
    }

    public User GetUser()
    {
        var users = GetUsers(new RandomUserParams());
        return users[0];
    }

    public List<User> GetUsers(int count)
    {
        var users = GetUsers(new RandomUserParams() { Count = count });
        return users;
    }

    public List<User> GetUsers(RandomUserParams userParams)
    {
        request = new RestRequest("", Method.Get);
        if (userParams.Count >= 0) { request.AddQueryParameter("results", userParams.Count); }
        if (userParams.Gender is not null) { request.AddQueryParameter("gender", userParams.Gender); }
        if (userParams.Country is not null) { request.AddQueryParameter("nat", userParams.Country); }
        if (userParams.Password is not null) { request.AddQueryParameter("password", userParams.Password); }
        if (userParams.Seed is not null) { request.AddQueryParameter("seed", userParams.Seed); }

        response = client.Execute(request);

        var RandomUserResponse = JsonSerializer.Deserialize<RandomUserResponse>(response.Content);
        var userJSON = JsonSerializer.Serialize(RandomUserResponse.Results);
        var users = JsonSerializer.Deserialize<List<User>>(userJSON);

        return users;
    }

}
