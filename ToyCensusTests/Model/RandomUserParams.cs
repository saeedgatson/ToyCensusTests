using System.Diagnostics.Metrics;
using System.Reflection;

namespace ToyCensusTests.Model;

public class RandomUserParams
{
    public int Count { get; set; }
    public string Gender { get; set; }
    public string Country { get; set; }
    public string Password { get; set; }
    public string Seed { get; set; }
    
    public RandomUserParams()
    {
        Count = 1;
        Gender = "";
        Country = "MX";
        Password = "";
        Seed = "saeed";
    }

    public RandomUserParams(int count, string gender, string country, string password, string seed)
    {
        Count = count;
        Gender = gender;
        Country = country;
        Password = password;
        Seed = seed;
    }
}
