# Dependencies
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
