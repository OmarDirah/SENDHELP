# Server Documentation 
## Classes
---
## Player
__Members__
playerID
- int
- Server Generated

username 
- String 
- Client Defined

Create a new player object 
```cs
public Player currentUser = new Player("Player1");
```
##### Methods
Set player Data
```cs
Player.Post(Player playerObj);
```
- void
- Pass in playerObj as a parameter to add to database

Get player Data
```cs
Player.Get(playerID, return_feild, *return_type)
```
- playerID: Primary Key of player
- return feild: What columns to parse
- Data type. Default = string

_Example: Get the username from a player_
```cs
currentPlayer.username = GetPlayer(playerID, "username", "string")
```
---
#### Game
gameID
- int
- Server Generated

playerID
- int
- Server Generated

difficulty
- string
- Client defined

score 
- int 
- Gameplay derrived

timeStarted 
- TimeStamp 
- Server Generated

timeCompleted 
- TimeStamp 
- Server Generated

Create a new Game object 
- Pass playerID into constructor, other attributes are server generated
```cs
public Game currentGame = new Game(Player1.playerID);
```
---
#### Level
gameID
- int
- Server Generated

playerID
- int
- Server Generated

levelNum
- int
- Program derived

difficulty
- string
- Client defined

levelScore 
- int 
- Gameplay derived

timeStarted 
- TimeStamp 
- Server Generated

timeCompleted 
- TimeStamp 
- Server Generated

Create new Level object
- Pass gameID, playerID, levelNum into constructor, other attributes are server generated
```cs
private int level_Number = 1;
public Level currentLevel = new Level(currentGame.gameID, Player1.playerID, level_Number);
```
---
#### Score
gameID
- int
- Server Generated

playerID
- int
- Server Generated

username
- string

datePlayed
- Date
- Derived from timeCompleted timestamp from Game table

Create Score object
```cs
Score scoreObj = new Score(currentGame.gameID);
```
##### Methods
Get highscores
```cs
public Score[] GetHighScores(int arrayLength);
```
_Example: Get array of highscores_
```cs
Score[] highscores;
highscores = Score.GetHighScores(10);
```

---
## Developer Usage
Requirements:
- API KEY 
- API URL 