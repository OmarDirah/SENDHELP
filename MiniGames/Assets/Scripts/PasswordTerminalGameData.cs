using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasswordTerminalGameData : MonoBehaviour
{
    int level;
    int index;
    Screen currentScreen;
    string password;

    List<string> answerPool = new List<string>();
    List<string> completedModules = new List<string>();

    List<string> uniquenessWrong = new List<string>() { "qwerty", "aabbcc", "admin", "password", "example", "lkjhg" };
    List <string> uniquenessRight = new List<string>() { "pfimtcz", "tdmaydcf", "pltadcaq", "qtfasda", "jfczxlf", "sdosanb"};

    List<string> lengthWrong = new List<string>() { "qwerty", "admin", "abc123", "passwrd", "omgpop", "unknown", "pokemon", "soccer" };
    List<string> lengthRight = new List<string>() { "sunshine", "password123", "0123456789", "ilikestuff", "football"};

    List<string> complexityWrong = new List<string>() { "unknown", "sunshinE", "Default", "shadow", "basketBall" };
    List<string> complexityRight = new List<string>() { "pa$w0rd", "ch0co!at3", "i<3games" };

    List<string> secrecyWrong = new List<string>() { "mainStreet", "Poughkeepsie", "January1", "Smith",  "hometown"};
    List<string> secrecyRight = new List<string>() { "IAcceptTheRisk", "openSesame", "Whatever!", "genericTerms" };

    List<string> passphrasesWrong = new List<string>() { "one word", "swapping 4 with A", "using @ for at", "birth month" };
    List<string> passphrasesRight = new List<string>() { "song lyrics", "long sentence", "quotation" };

    List<string> multifactorWrong = new List<string>() { "password", "2 passwords", "fingerprint", "email" };
    List<string> multifactorRight = new List<string>() { "password&fingerprint", "password&email", "phone&fingerprint", "password&phone", "password&phone&email" };

    List<string> goodpracticeWrong = new List<string>() { "write on wall", "tell others", "shared passwords" };
    List<string> goodpracticeRight = new List<string>() { "change regularly", "use secure machines" };

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
        PasswordTerminalTerminal.ClearScreen();
        PasswordTerminalTerminal.WriteLine("So you need help creating a new password...");
        PasswordTerminalTerminal.WriteLine(" ");
        PasswordTerminalTerminal.WriteLine("You have a lot to learn ARI. Where would you like to begin?");
        PasswordTerminalTerminal.WriteLine(" ");
        PasswordTerminalTerminal.WriteLine("Completed modules: " + string.Join(", ", completedModules) );
        PasswordTerminalTerminal.WriteLine(" ");
        PasswordTerminalTerminal.WriteLine("Enter '1' for 'Uniqueness'");
        PasswordTerminalTerminal.WriteLine("Enter '2' for 'Length'");
        PasswordTerminalTerminal.WriteLine("Enter '3' for 'Complexity'");
        PasswordTerminalTerminal.WriteLine("Enter '4' for 'Secrecy'");
        PasswordTerminalTerminal.WriteLine("Enter '5' for 'Passphrases'");
        PasswordTerminalTerminal.WriteLine("Enter '6' for 'Multifactor Authentication'");
        PasswordTerminalTerminal.WriteLine("Enter '7' for 'Good Practices'");
      
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
            SetRandomPassword();
            ShowModulePage();
        }
        else
        {
            PasswordTerminalTerminal.WriteLine("Please enter a valid strage number");
        }
    }

    void ShowModulePage()
    {
        PasswordTerminalTerminal.ClearScreen();

        switch (level)
        {
            case 1:
                currentScreen = Screen.Uniqueness;
                UniquenessText();
                DisplayChoices();
                break;
            case 2:
                currentScreen = Screen.Length;
                LengthText();
                DisplayChoices();
                break;
            case 3:
                currentScreen = Screen.Complexity;
                ComplexityText();
                DisplayChoices();
                break;
            case 4:
                currentScreen = Screen.Secrecy;
                SecrecyText();
                DisplayChoices();
                break;
            case 5:
                currentScreen = Screen.Passphrases;
                PassphrasesText();
                DisplayChoices();
                break;
            case 6:
                currentScreen = Screen.MultiFactor;
                MultifactorText();
                DisplayChoices();
                break;
            case 7:
                currentScreen = Screen.GoodPractice;
                GoodPracticeText();
                DisplayChoices();
                break;
        }
    }

    void SetRandomPassword()
    {
        answerPool.Clear();

        switch (level)
        {
            case 1:
                password = uniquenessRight[Random.Range(0, uniquenessRight.Count)];
                answerPool.Add(password);
                IncludeBadAnswers(uniquenessWrong);
                break;
            case 2:
                password = lengthRight[Random.Range(0, lengthRight.Count)];
                answerPool.Add(password);
                IncludeBadAnswers(lengthWrong);
                break;
            case 3:
                password = complexityRight[Random.Range(0, complexityRight.Count)];
                answerPool.Add(password);
                IncludeBadAnswers(complexityWrong);
                break;
            case 4:
                password = secrecyRight[Random.Range(0, secrecyRight.Count)];
                answerPool.Add(password);
                IncludeBadAnswers(secrecyWrong);
                break;            
            case 5:
                password = passphrasesRight[Random.Range(0, passphrasesRight.Count)];
                answerPool.Add(password);
                IncludeBadAnswers(passphrasesWrong);
                break;
            case 6:
                password = multifactorRight[Random.Range(0, multifactorRight.Count)];
                answerPool.Add(password);
                IncludeBadAnswers(multifactorWrong);
                break;           
            case 7:
                password = goodpracticeRight[Random.Range(0, goodpracticeRight.Count)];
                answerPool.Add(password);
                IncludeBadAnswers(goodpracticeWrong);
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

            completedModules.Sort();

            CheckWinConditions();
        }
        else if (Input.ToLower() == "back")
        {
            currentScreen = Screen.StartingScreen;
            ShowStartingScreen();
        }
        else
        {
            ShowModulePage();
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
        PasswordTerminalTerminal.ClearScreen();
        PasswordTerminalTerminal.WriteLine("You have successfully completed all the modules");
    }

    void IncludeBadAnswers(List<string> theList)
    {
        Debug.Log("ANSWER: " + answerPool[0]);

        int index1 = Random.Range(0, theList.Count);
        string value1 = theList[index1];
        theList.RemoveAt(index1);

        int index2 = Random.Range(0, theList.Count);
        string value2 = theList[index2];

        answerPool.Add(value1);
        answerPool.Add(value2);

        theList.Add(value1);       
    }

    void DisplayChoices()
    {
        index = Random.Range(0, answerPool.Count);
        string value1 = answerPool[index];
        PasswordTerminalTerminal.WriteLine(" - " + value1);
        answerPool.RemoveAt(index);

        index = Random.Range(0, answerPool.Count);
        string value2 = answerPool[index];
        PasswordTerminalTerminal.WriteLine(" - " + value2);
        answerPool.RemoveAt(index);

        index = Random.Range(0, answerPool.Count);
        string value3 = answerPool[index];
        PasswordTerminalTerminal.WriteLine(" - " + value3);
        answerPool.RemoveAt(index);

        answerPool.Add(value1);
        answerPool.Add(value2);
        answerPool.Add(value3);
    }

    void UniquenessText()
    {
        PasswordTerminalTerminal.WriteLine("~~ UNIQUENESS ~~");
        PasswordTerminalTerminal.WriteLine("A crucical aspect to a secure password is that it is unique. Unique being as close to one of a kind as possible.");
        PasswordTerminalTerminal.WriteLine("Dictionary attacks are common ways to crack      passwords and those use a list of known passwords.");
        PasswordTerminalTerminal.WriteLine("Unique passwords are unlikely to appear on that  list, making it safe.");
        PasswordTerminalTerminal.WriteLine(" ");
        PasswordTerminalTerminal.WriteLine("Knowing this, which of the following passwords is most unique?");
    }

    void LengthText()
    {
        PasswordTerminalTerminal.WriteLine("~~ LENGTH ~~");
        PasswordTerminalTerminal.WriteLine("Increasing your password length is the easiest");
        PasswordTerminalTerminal.WriteLine("way to increase the password space. The more");
        PasswordTerminalTerminal.WriteLine("available passwords means the longer it will taketo crack it. Therefore, the more the merrier.");
        PasswordTerminalTerminal.WriteLine("Current standards require at least 8, but aim for12.");
        PasswordTerminalTerminal.WriteLine(" ");
        PasswordTerminalTerminal.WriteLine("The standards for length are best met by which   password?");
    }

    void ComplexityText()
    {
        PasswordTerminalTerminal.WriteLine("~~ COMPLEXITY ~~");
        PasswordTerminalTerminal.WriteLine("Passwords ought to be more than just letters.    Mixing upper and lower case is a start. ");
        PasswordTerminalTerminal.WriteLine("Incorporating special characters and numbers is  also a very good way to increase security. ");
        PasswordTerminalTerminal.WriteLine("Simple like adding punctuation throughout or     replacing an s with $ is a decent start.");
        PasswordTerminalTerminal.WriteLine(" ");
        PasswordTerminalTerminal.WriteLine("The complexity requirement is best met by which  password?");
    }

    void SecrecyText()
    {
        PasswordTerminalTerminal.WriteLine("~~ SECRECY ~~");
        PasswordTerminalTerminal.WriteLine("Attackers can research basic information about   their targets. Important that one should not use any identifiable information as their password.");
        PasswordTerminalTerminal.WriteLine("Additionally, in a data breach you would not wantto release more information about  yourself.");
        PasswordTerminalTerminal.WriteLine("Abiding by the secrecy means it should be hard   for someone to guess. Avoid common passwords and  identifiable information.");
        PasswordTerminalTerminal.WriteLine(" ");
        PasswordTerminalTerminal.WriteLine("Which of the following is the most secret?");
    }

    void PassphrasesText()
    {
        PasswordTerminalTerminal.WriteLine("~~ PASSPHRASES ~~");
        PasswordTerminalTerminal.WriteLine("There are a lot of requirements to making a good password and it can be hard to use them all. ");
        PasswordTerminalTerminal.WriteLine("Passphrase turns a simple sentence into a more   secure password.");
        PasswordTerminalTerminal.WriteLine("The sentence - I really like to eat cold pizza - would become IRLTECP and from there mix cases and add special characters.");
        PasswordTerminalTerminal.WriteLine("Using a known sentence is easy to remember and   hard to guess.");
        PasswordTerminalTerminal.WriteLine(" ");
        PasswordTerminalTerminal.WriteLine("Which would turn into a good passphrase?");
    }

    void MultifactorText()
    {
        PasswordTerminalTerminal.WriteLine("~~ MULTIFACTOR AUTHENTICATION ~~");
        PasswordTerminalTerminal.WriteLine("There are more to passwords than just a string ofcharacters. That string serves as one factor of   authorization. Also known as 'what you know'.");
        PasswordTerminalTerminal.WriteLine("Multifactor authentication can include 'what you are' (e.g. fingerprint) and 'what you have' (e.g. a phone). Using multiple factors is more secure   than just a password");
        PasswordTerminalTerminal.WriteLine(" ");
        PasswordTerminalTerminal.WriteLine("Maybe we can start to include this on our station"); 
        PasswordTerminalTerminal.WriteLine("What combination provides the most security?");
    }

    void GoodPracticeText()
    {
        PasswordTerminalTerminal.WriteLine("~~ GOOD PRACTICES ~~");
        PasswordTerminalTerminal.WriteLine("Once a good password is created, you need tomake sure it stays safe. Using password managers       instead of sticky notes is one great way. Also    avoiding public terminals and password reuse can  help.");
        PasswordTerminalTerminal.WriteLine(" ");
        PasswordTerminalTerminal.WriteLine("Out of the following, which would you recommend?");
    }

}
