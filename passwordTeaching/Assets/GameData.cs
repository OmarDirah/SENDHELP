using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    int level;
    Screen currentScreen;
    string password;

    string[] level1Passwords = { "one", "two" };
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
        if (resetPassword == "yes")
        {
            SetRandomPassword();
        }
        switch (level)
        {
            case 1:
                currentScreen = Screen.Uniqueness;
                Terminal.WriteLine("UNIQUENESS -");
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
                password = level1Passwords[Random.Range(0, level1Passwords.Length)];
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
        if (Input == password)
        {
            DisplayWinScreen();
        }
        else if (Input.ToLower() == "back")
        {
            currentScreen = Screen.StartingScreen;
            ShowStartingScreen();
        }
        else
        {
            ShowModulePage("no");
        }
    }

    void DisplayWinScreen()
    {
        currentScreen = Screen.Completed;
        Terminal.ClearScreen();
        Terminal.WriteLine("You have successfully completed all the modules");
    }


}
