using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ToyCensusTests.Model.Random;

namespace ToyCensusTests.Model;


//CensusToyPost myDeserializedClass = JsonSerializer.Deserialize<CensusToyPost>(myJsonResponse);
public class CensusToyPost
{
    [JsonPropertyName("actionType")]
    public string ActionType { get; set; }

    [JsonPropertyName("top")]
    public int Top { get; set; }

    [JsonPropertyName("users")]
    public List<User> Users { get; set; }
}

public class User
{
    [JsonPropertyName("gender")]
    public string Gender { get; set; }

    [JsonPropertyName("name")]
    public Name Name { get; set; }

    [JsonPropertyName("location")]
    public Location Location { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("login")]
    public Login Login { get; set; }

    [JsonPropertyName("dob")]
    public Dob Dob { get; set; }

    [JsonPropertyName("registered")]
    public string Registered { get; set; }

    [JsonPropertyName("phone")]
    public string Phone { get; set; }

    [JsonPropertyName("cell")]
    public string Cell { get; set; }

    [JsonPropertyName("id")]
    public Id Id { get; set; }

    [JsonPropertyName("picture")]
    public Picture Picture { get; set; }

    [JsonPropertyName("nat")]
    public string Nat { get; set; }
}

public class Id
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("value")]
    public string Value { get; set; }
}

public class Location
{
    [JsonPropertyName("street")]
    public Street Street { get; set; }

    [JsonPropertyName("city")]
    public string City { get; set; }

    [JsonPropertyName("state")]
    public string State { get; set; }

    [JsonPropertyName("postcode")]
    public int Postcode { get; set; }

    [JsonPropertyName("coordinates")]
    public Coordinates Coordinates { get; set; }

    [JsonPropertyName("timezone")]
    public Timezone Timezone { get; set; }
}

public class Login
{
    [JsonPropertyName("username")]
    public string Username { get; set; }

    [JsonPropertyName("password")]
    public string Password { get; set; }

    [JsonPropertyName("salt")]
    public string Salt { get; set; }

    [JsonPropertyName("md5")]
    public string Md5 { get; set; }

    [JsonPropertyName("sha1")]
    public string Sha1 { get; set; }

    [JsonPropertyName("sha256")]
    public string Sha256 { get; set; }
}

public class Name
{
    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("first")]
    public string First { get; set; }

    [JsonPropertyName("last")]
    public string Last { get; set; }
}

public class Picture
{
    [JsonPropertyName("large")]
    public string Large { get; set; }

    [JsonPropertyName("medium")]
    public string Medium { get; set; }

    [JsonPropertyName("thumbnail")]
    public string Thumbnail { get; set; }
}

public class Street
{
    [JsonPropertyName("number")]
    public int? Number { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }
}

public class Dob
{
    [JsonPropertyName("date")]
    public DateTime? Date { get; set; }

    [JsonPropertyName("age")]
    public int? Age { get; set; }
}
