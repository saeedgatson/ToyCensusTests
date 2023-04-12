using ToyCensusTests.Model;

namespace ToyCensusTests.Utilites;

public class ToyCensusClient : RestSharp.RestClient
{
    private RestClient client;
    private RestRequest request;
    private RestResponse response;
    private const string BaseUrl = "https://census-toy.nceng.net/prod/toy-census";

    public ToyCensusClient()
    {
        this.client = new RestClient(BaseUrl);
    }

    public RestRequest CreatePostRequest(string type, int top)
    {
        RandomUserClient randomUserClient = new RandomUserClient();
        User user = randomUserClient.GetUser();
        request = new RestRequest("", Method.Post);
        request.AddJsonBody(new CensusToyPost() { ActionType = type, Top = top, Users = new List<User>() { user } });
        return this.request;
    }

    public RestRequest CreatePostRequest(string type, int top, RandomUserParams userParams)
    {
        RandomUserClient randomUserClient = new RandomUserClient();
        List<User> users = randomUserClient.GetUsers(userParams);
        request = new RestRequest("", Method.Post);
        request.AddJsonBody(new CensusToyPost() { ActionType = type, Top = top, Users = users });
        return this.request;
    }

    public RestResponse SubmitRequest()
    {
        response = client.Execute(this.request);
        return this.response;
    }
}
