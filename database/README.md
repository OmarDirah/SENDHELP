# ARI Database Class
## _SendHelp game Studios_

Logo
Build Status
Version
---

The ARIdatabaseClass is responsible for the Data integrity and organization of the
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
const URL = //endpointURL
const KEY = //yourKey

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
readonly URL = //myurl
readonly KEY = //myKey

var client = new RestClient(URL + KEY);
var request = new RestRequest(Method.GET);
IRestResponse response = client.Execute(request);
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
