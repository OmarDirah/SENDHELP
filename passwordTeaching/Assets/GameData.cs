using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    int level;
    Screen currentScreen;
    string password;

    List<string> answerPool = new List<string>();
    List<string> completedModules = new List<string>();

    List<string> uniquenessWrong = new List<string>() { "qwerty", "aabbcc", "admin", "password", "example", "lkjhg" };
    List <string> uniquenessRight = new List<string>() { "pfimtcz", "tdmaydcf", "pltadcaq", "qtfasda", "jfczxlf", "sdosanb"};
    string[] level2Passwords = { "three", "four" };

    enum Screen
    {
        StartingScreen, 
        Uniqueness,
        Length,
        Complexity,
        Secrecy,
        Passphrases,
        MultiFactor,
        GoodPractice,
        Completed
    }

    bool uniqueness, length, complexity, secrecy, passphrases, multifactor, goodpractice = false;

    void Start()
    {
        ShowStartingScreen();
    }

    void ShowStartingScreen()
    {
        currentScreen = Screen.StartingScreen;
        Terminal.ClearScreen();
        Terminal.WriteLine("So you need help creating a new password...");
        Terminal.WriteLine(" ");
        Terminal.WriteLine("You have a lot to learn ARI. Where would you like to begin?");
        Terminal.WriteLine(" ");
        Terminal.WriteLine("Completed modules: " + string.Join(", ", completedModules) );
        Terminal.WriteLine(" ");
        Terminal.WriteLine("Enter '1' for 'Uniqueness'");
        Terminal.WriteLine("Enter '2' for 'Length'");
        Terminal.WriteLine("Enter '3' for 'Complexity'");
        Terminal.WriteLine("Enter '4' for 'Secrecy'");
        Terminal.WriteLine("Enter '5' for 'Passphrases'");
        Terminal.WriteLine("Enter '6' for 'Multifactor Authentication'");
        Terminal.WriteLine("Enter '7' for 'Good Practices'");
      
    }

    void OnUserInput(string Input)
    {
        Debug.Log("ON INPUT");
        if (Input == "home")
        {
            ShowStartingScreen();
        }
        else if (Input.ToLower() == "quit" || Input.ToLower() == "exit" || Input.ToLower() == "close" || Input.ToLower() == "leave")
        {
            Application.Quit();
        }
        else if (currentScreen == Screen.StartingScreen)
        {
            RunStartingScreen(Input);
        }
        else if ( (currentScreen == Screen.Uniqueness) ||
                    (currentScreen == Screen.Length) ||
                    (currentScreen == Screen.Complexity) ||
                    (currentScreen == Screen.Secrecy) ||
                    (currentScreen == Screen.Passphrases) ||
                    (currentScreen == Screen.MultiFactor) ||
                    (currentScreen == Screen.GoodPractice) )
        {
            CheckAnswer(Input);
        }
    }
    
    void RunStartingScreen(string Input)
    {
        bool _isValid = false;
        int InputAsInt = int.Parse(Input);

        if ( (1 <= InputAsInt) && (InputAsInt <= 7) )
        {
            _isValid = true;
        }

        if (_isValid)
        {
            level = InputAsInt;
            ShowModulePage();
        }
        else
        {
            Terminal.WriteLine("Please enter a valid strage number");
        }
    }

    void ShowModulePage(string resetPassword = "yes")
    {
        Terminal.ClearScreen();
        Debug.Log("RESET PASSWORD PARAM: " + resetPassword);
        Debug.Log("ANSWER BEFORE IF/ELSE: " + password);
        if (resetPassword == "yes")
        {
            Debug.Log("IN RANDOM RESET YES");
            SetRandomPassword();
        }
        else
        {
            Debug.Log("IN RANDOM RESET NO");
            Debug.Log("ANSWER: " + password);
        }
        Debug.Log("IN SWITCH FOR LEVEL " + level);
        switch (level)
        {
            case 1:
                currentScreen = Screen.Uniqueness;
                UniquenessText();
                break;
            case 2:
                currentScreen = Screen.Length;
                Terminal.WriteLine("LENGTH -");
                break;
            case 3:
                currentScreen = Screen.Complexity;
                Terminal.WriteLine("COMPLEXITY -");
                break;
            case 4:
                currentScreen = Screen.Secrecy;
                Terminal.WriteLine("SECRECY -");
                break;
            case 5:
                currentScreen = Screen.Passphrases;
                Terminal.WriteLine("PASSPHRASES -");
                break;
            case 6:
                currentScreen = Screen.MultiFactor;
                Terminal.WriteLine("MULTIFACTOR AUTHENTICATION -");
                break;
            case 7:
                currentScreen = Screen.GoodPractice;
                Terminal.WriteLine("GOOD PRACTICES -");
                break;
        }
    }

    void SetRandomPassword()
    {
        switch (level)
        {
            case 1:
                password = uniquenessRight[Random.Range(0, uniquenessRight.Count)];
                answerPool.Add(password);
                IncludeBadAnswers(uniquenessWrong);
                Debug.Log("PASSWORD " + password);
                Debug.Log("ANSWER POOL " + answerPool);
                break;
            case 2:
                password = level2Passwords[Random.Range(0, level2Passwords.Length)];
                break;
            default:
                Debug.Log("Invalid Level");
                break;
        }
    }

    void CheckAnswer(string Input)
    {
        Debug.Log("ON CHECK ANSWER");
        if (Input == password)
        {
            switch (currentScreen)
            {
                case Screen.Uniqueness:
                    uniqueness = true;
                    if (!completedModules.Contains("1"))
                        completedModules.Add("1");
                    break;
                case Screen.Length:
                    length = true;
                    if (!completedModules.Contains("2"))
                        completedModules.Add("2");
                    break;
                case Screen.Complexity:
                    complexity = true;
                    if (!completedModules.Contains("3"))
                        completedModules.Add("3");
                    break;
                case Screen.Secrecy:
                    secrecy = true;
                    if (!completedModules.Contains("4"))
                        completedModules.Add("4");
                    break;
                case Screen.Passphrases:
                    passphrases = true;
                    if (!completedModules.Contains("5"))
                        completedModules.Add("5");
                    break;
                case Screen.MultiFactor:
                    multifactor = true;
                    if (!completedModules.Contains("6"))
                        completedModules.Add("6");
                    break;
                case Screen.GoodPractice:
                    goodpractice = true;
                    if (!completedModules.Contains("7"))
                        completedModules.Add("7");
                    break;
                default:
                    Debug.Log("HUH?");
                    break;
            }

            CheckWinConditions();
        }
        else if (Input.ToLower() == "back")
        {
            currentScreen = Screen.StartingScreen;
            ShowStartingScreen();
        }
        else
        {
            Debug.Log("SHOW MODULE PAGE ELSE REDIRECTION");
            ShowModulePage("no");
        }
    }

    void CheckWinConditions()
    {
        if (uniqueness && length && complexity && secrecy && passphrases && multifactor && goodpractice)
        {
            DisplayWinScreen();
        }
        else
        {
            ShowStartingScreen();
        }
    }

    void DisplayWinScreen()
    {
        currentScreen = Screen.Completed;
        Terminal.ClearScreen();
        Terminal.WriteLine("You have successfully completed all the modules");
    }

    void IncludeBadAnswers(List<string> theList)
    {
        int index1 = Random.Range(0, theList.Count);
        string value1 = theList[index1];
        theList.RemoveAt(index1);

        int index2 = Random.Range(0, theList.Count);
        string value2 = theList[index2];

        answerPool.Add(value1);
        answerPool.Add(value2);

        theList.Add(value1);
    }

    void UniquenessText()
    {
        Terminal.WriteLine("UNIQUENESS -");
        Terminal.WriteLine("A crucical aspect to a secure password is that it is unique. Unique being as close to one of a kind as possible.");
        Terminal.WriteLine("Dictionary attacks are common ways to crack      passwords and those use a list of known passwords.");
        Terminal.WriteLine("Unique passwords are unlikely to appear on that  list, making it safe.");
        Terminal.WriteLine(" ");
        Terminal.WriteLine("Knowing this, which of the following passwords is most unique.");

        int index1 = Random.Range(0, answerPool.Count);
        string value1 = answerPool[index1];
        Terminal.WriteLine(" - " + value1);
        answerPool.RemoveAt(index1);

        int index2 = Random.Range(0, answerPool.Count);
        string value2 = answerPool[index2];
        Terminal.WriteLine(" - " + value2);
        answerPool.RemoveAt(index2);

        int index3 = Random.Range(0, answerPool.Count);
        string value3 = answerPool[index3];
        Terminal.WriteLine(" - " + value3);
        answerPool.RemoveAt(index3);

        answerPool.Add(value1);
        answerPool.Add(value2);
        answerPool.Add(value3);
    }


}
