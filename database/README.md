# ARI Database Class
## _SendHelp game Studios_

Logo
Build Status
Version

The ARIdatabaseClass is responsible for the Data integrity and organization of the
ARI Cyber Security educational video game
- Storing records of game performance for optimization
- Maintain game data and provide the developers and game designers insight of gameplay

## Features
- Set
- Get 
- Fetch UI Data
- Queries for developer insights

## Requirements
- [.NET 5.0 Framework] 

## Tech
The ARI Database uses a number of open source projects to work properly:

- [AWS API Gateway] - For backend database communication 
- [AWS DynamoDB] - To store game, player and other data for gameplay optimization. 
- Developer portal - Coming soon...

## Usage

##### Example JSON GET Request 
JavaScript
```js
const URL = endpointURL;
const KEY = yourKey;

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

C#
```cs
readonly URL = myurl;
readonly KEY = myKey;

var client = new RestClient(URL + KEY);
var request = new RestRequest(Method.GET);
IRestResponse response = client.Execute(request);
```

[//]: # (These are reference links used in the body)

   [AWS API Gateway]: <https://us-east-2.console.aws.amazon.com/apigateway/main/apis?region=us-east-2>
   [AWS DynamoDB]: <https://us-east-2.console.aws.amazon.com/dynamodbv2/home?region=us-east-2#service>
   [AriCyberThink.com]: <http://aricyberthink.com/>
   [.NET 5.0 Framework]: <https://dotnet.microsoft.com/download/dotnet/5.0>
