# ARI Server Class
## _SendHelp game Studios_

Logo
Build Status
Version
---

This class is responsible for the Data integrity and organization of the
ARI Cyber Security educational video game
- Storing records of game performance for optimization
- Maintain game data and provide the developers and game designers insight of gameplay
---

## Features
- Set
- Get 
- Fetch UI Data
- Queries for developer insights
---

## Requirements
- [.NET 5.0 Framework] 
---

## Tech
The ARI Database uses a number of open source projects to work properly:

- [AWS API Gateway] - For backend database communication 
- [AWS DynamoDB] - To store game, player and other data for gameplay optimization. 
- Developer portal - Coming soon...

---
## Usage

### AWS DynamoDB
#### Sample Table 

gameID|playerID|score|difficulty|
-------|-------|--------|--------
__213__|643|241|easy
__214__|324|874|intermediate

##### Example JSON GET Request 
__JavaScript__

```js
const URL; //your endpoint URL
const KEY; //your API key

fetch(URL + KEY, {
  "method": "GET",
  "headers": {}
})
.then(response => {
  console.log(response);
})
.catch(err => {
  console.error(err);
});
```

__C#__
```cs
readonly URI; //your endpoint URI
readonly KEY; //your API key

var client = new HttpClient();
var request = new HttpRequestMessage
{
    Method = HttpMethod.Get,
    RequestUri = new Uri("http://localhost:8080/api/courses"),
};
using (var response = await client.SendAsync(request))
{
    response.EnsureSuccessStatusCode();
    var body = await response.Content.ReadAsStringAsync();
    Console.WriteLine(body);
}
```

Output:
```json
[
  {
    "gameID": 213,
    "playerID": 643,
    "score": 241,
    "difficulty": "easy"
  },
  {
    "gameID": 214,
    "playerID": 324,
    "score": 874,
    "difficulty": "intermediate"
  }
]
```


[//]: # (These are reference links used in the body)

   [AWS API Gateway]: <https://us-east-2.console.aws.amazon.com/apigateway/main/apis?region=us-east-2>
   [AWS DynamoDB]: <https://us-east-2.console.aws.amazon.com/dynamodbv2/home?region=us-east-2#service>
   [AriCyberThink.com]: <http://aricyberthink.com/>
   [.NET 5.0 Framework]: <https://dotnet.microsoft.com/download/dotnet/5.0>
