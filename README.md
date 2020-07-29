# RestSharp Sample Implementation
Base REST request service for personal projects.

GET samples:
```csharp
var response = await new RequestService()
{
  Method = Method.GET,
  URL = "https://logikoz.net",
  URN = "Tests"
}.ExecuteTaskAsync();
```

With jwt authorization header:
```csharp
var response = await new RequestService()
{
  Method = Method.GET,
  URL = "https://logikoz.net",
  URN = "Tests",
  Authenticator = new JwtAuthenticator("123") // result: Bearer 123
}.ExecuteTaskAsync();
```

With parameters:
```csharp
var request = new RequestService()
{
  Method = Method.GET,
  URL = "https://logikoz.net",
  URN = "Tests",
};

request.Parameters.Add("param1", "123");
request.Parameters.Add("param2", "123");
request.Parameters.Add("param3", "123");

IRestResponse response = await request.ExecuteTaskAsync();
```

POST samples:
```csharp
var response = await new RequestService()
{
  Method = Method.POST,
  URL = "https://logikoz.net",
  URN = "Tests/add",
  Body = obj //c # object, no need to serialize.
}.ExecuteTaskAsync();
```

For PUT, Delete, Options... methods, follow the same principle as the examples cited above.
