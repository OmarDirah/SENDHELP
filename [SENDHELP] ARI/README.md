# Code Specifications
---
## Dialogue
### Members

- This class will store data objects to store conversational and dialogue data 

#### names
``` String[] ```
-	This is the object that will store the names of the 2 parties that are interacting within the game


#### sentences
``` String[] ```
-	The object that will hold the conversational sentences to be queued, whilst a conversation is happening 

--- 

## DialogueManager 
- This class acts as a controller for all dialogue events 

### Members

#### nameText
``` Text ```
-	The name of the object to be displayed while a conversation is happening 

#### dialogueText
``` Text ```
-	Description needed

#### conversation
``` Animator ```
-	Description needed

#### names
```Queue[] String```
-	Description needed


#### sentences
```Queue, String```
-	Description needed

## Methods
```cs
Start();
```
-	Called on loading of the scene
-	Initialize names and sentences

 
```cs
StartDialogue(Dialogue dialogue);
```
-	Use the Unity object conversation, and set the current gamestate to ```conversation```

-	Reset the ```names```, and ```sentences``` queues. 
  
-	Iterate through the ```dialogue.names```, and enque and print the current name. 

-	Iterate through the ```sentences``` queue, enqueue and print the current sentence.
       
```cs
DisplayNextName();
```
-	Parse the ```names``` queue. If there are none left, end the conversation by calling ```EndDialogue();```   

-	If names exist in the ```names``` queue, set the ```nameText``` to that name. 

```cs
DisplayNextSentence();
```
-	Parse the ```names``` queue. If there are none left, end the conversation by calling ```EndDialogue();```   
-	Parse the ```sentences``` queue, and display the sentence that is next in the queue. 
-	Stop all other Unity’s Coroutines by calling 
```StopAllCoroutines();```
-   Start a new routine by calling 
```StartCoroutine(TypeSentence(sentence));``` 

```cs
TypeSentence(string sentence);
```
-	Convert the next sentence in the ```sentence``` Queue to a Char Array, and print each character one at a time. 
-	By doing so, printing one letter at a time presents in a much more aesthetic fashion to the user, rather than presenting one whole word block at a time. 
-	Text will also be easier to read, and at a consistent speed.
    

```cs
EndDialogue()
```
-	Set the conversation property to false and pass

---

## DialoguePrompt 
---

__This class will be respobile for determining the players current environment, ## and execute logic for which dialogue should be displayed.__

### Members

### animators
- Description needed

### num
``` int ```

### convoNum
``` int ```

## Classes

```cs
OnTriggerEnter(Collider other)
```
-	When a player enters an area of the environment, or makes physical contact with an object in the environment load a prompt window and display the desired dialogue
-	Utilizes Switch cases to determine the current state of the game
-	To determine which method to call, uses ```CompareTag```

```cs
UIAppear(int num)
```
Parameter: ```num```
-   Designates which event to trigger
-	Uses the num argument to trigger UI with a dialogue event logic for particular cases.


```cs
UIDisappear(int num)
```
- Triggers how to dialogue event will be closed.

---

## DialogueTrigger
-	Class type: MonoBehaviour

### Members

#### Dialogue
-	Of type ```dialogue```

## Methods

```cs
TriggerDialogue()
```

-	Uses Unity's built in methods ```FindObjectOfType<DialogueManager>()```

-	And calls the method ```StartDialogue(dialogue)``` to trigger a dialogue event
---
    
# AccessGranted 
-	Of type ```Interactable```
-	This class will handle logic for allowing the player to explore further if they posses the object required for that area access. 

### Members
#### anim
-	type: ```Animator``` 

```cs
Start()
```
-	Called immediately as the scene loads. 
-	Initializes ```enabled``` to false.
   
```cs
Interact()
```
-	Sets ```anim.hasAccessKey``` to true
-	This triggers logic that will allow the player to explore further if they have the access key 
    
---

## PropPrompt
-	Of type ```MonoBehaviour```
-	This class handles logic for raising or closing a prompt based on a player event. 

### Members
-	eInteract
```Animator```

```cs
OnTriggerEnter(Collider other);
```
-	Given the ```Collider``` type, determine which UI should appear on the players screen.    

```cs
OnTriggerExit(Collider other);
```
-	Given the ```Collider``` type, determine which UI should appear on the players screen.    

```cs
UIAppear();
```
-	Once the player is within range of an interactable object, prompt them to interact with it.
-	Calls ```eInteract.SetBool("IsOpen", true);``` to set interaction to not open (false)
       
```cs
UIDisappear()
```
-	Hide the ```interact``` screen prompt when a player leaves range of an interactable object.
-	Calls ```eInteract.SetBool("IsOpen", false);``` to set the interaction to not open (false)

---
## SceneChangeInteract
-	Of type ```Interactable```
-	This class controls the logic of changing gamescenes when a certain game event is triggered. 

### Members

#### SceneToLoad
``` String ```
-	Specify which scene you want to load on an event trigger

#### delayTime
``` int ```
-	How many milliseconds to delay when changing scenes

#### levelDone
``` int ```
-	Indicated whether the level is completed or not

#### anim
``` Animator ```
-	Description needed 

#### source
``` AudioSource ```
-	Audio source object to use to play sound
    
#### clip
``` AudioClip ```
-	Specify which clip to play 

```cs
Start()
```
-	Initialize enabled to false

```cs
Interact()
```
-   Implements ```Override```
-	Immediate Change to Scene Specified by Name - Will change to accommodate for loading screens
-	Specifies which audio clip to play, set level to complete and assign a value to minigame status if needed. 

```cs
DelayedAction()
```

-	Specifies a delayed action using Unity’s built in classes
-	Utilizes ```SceneManager.LoadScene(SceneToLoad);``` to specify developer desired scene

---
## Player
-	Of type ```MonoBehaviour```
-	Stores all instances and classes necessary to track player movement, score and behavior 

### Members

#### levelActive

``` String ```	

- The current level the player is on 

```cs
SavePlayer()
```

- Utilizes Unity’s built in method ```SaveSystem.SavePlayer(this);``` to save the players position at instance ```i``` 

```cs
LoadPlayer()
```
-	Assigns ```SaveSystem.LoadPlayer();``` to the class member ```Playerdata.data``` object.
-	Assignes ```data.levelInstance``` to ```levelActive```
-	Loads an initializes the player into a specified position 
-	Initialize a Unity ```Vector3``` object which tracks the players x,y and z coordinates in the engine 
---

## PlayerData
-	Class to store a players level, inventory and current position within the engine. 

### Members

##### levelInstance

``` String ```
- Identify which level the player is currently on 

##### Inventory

- String array 
- All objects currently in the players inventory 

##### position
``` Float[] ``` 
- The X Y and Z Coordinates of the player 

### Methods

```cs
PlayerData(Player player)
```

- Creates a levelInstance by calling ```player.levelActive``` to fetch the current level

---

## SaveSystem
- This class is resposible for saving the player state externally, yet locally 

### Methods

```cs
public static void SavePlayer(Player player)
```
 
- Utilizes ```BinaryFormatter```
- Initialized immediately 
- This method is responsible for Initializing a player object, create a file and write the player data to the file locally.
- File type: ```Binary encoded```
- Closes stream upon initialization 

```cs
public static PlayerData LoadPlayer()
```
Returns ```PlayerData``` object 
   
- To fetch binary file:
    ```c
   string path = Application.persistentDataPath + "/player.playerprefs"; 
   ```
- Deserialize the stream, and save the data to the player object 
- Return ```null``` if file cannot be fetched.
---

## SaveTrigger 

- This class is responsible for detecting collisons and events within the engine that should trigger a save game
    
### Members    
##### saver

``` Animator ```

### Methods

```cs
void OnTriggerEnter(Collider other)
```
- Search for the correct Unity Tag using 
    ```cs
    other.CompareTag("LivingQuartersTerminalDialogue"))
    ```
- Then trigger the appropriate UI for a save game event     
    ```cs
    UIAppear();
    ```

```cs
void OnTriggerExit(Collider other)
```
- When a player exits proximity of a save game event, hide the UI for a save game
- 
    ```cs
    this.UIDisappear();
    ```

    
```cs
public void UIAppear()
```
- Set IsSavePoint to ```true```
-  ```cs
    saver.SetBool("IsSavePoint", true);
    ```
    
    
```cs
public void UIDisappear()
```
- Set IsSavePoint to ```false```
-    
    ```cs
    saver.SetBool("IsSavePoint", false);
    ```   