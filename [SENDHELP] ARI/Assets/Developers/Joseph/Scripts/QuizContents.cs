using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizContents : MonoBehaviour
{
    // VARIABLES
    public string SceneToLoad;
    int currQuestion = 2;
    int currLevel = 1;
    Button thisButton;

    public Animator anim;
    public int delayTime;

    Button nextButton;

    bool firstTry = true;
    public GameObject caesarCipherImage;

    List<bool> level1UserCorrectness = new List<bool>()
    {
        //false, //1
        false, //2
        false, //3
        false, //4
        false, //5
        false, //6
        false, //7
        false, //8
        false, //9
        false, //10
        false, //11
    };

    Text questionNumber, question, option1, option2, option3, option4, option5;
    Button button1, button2, button3, button4, button5;

    List<string> level1questionNumberList = new List<string>() {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10"};

    List<string> level1questionList = new List<string>()
    {
        //"What should a good password include?",  //THIS IS MULTIPLE CORRECT, SO IGNORE FOR NOW
        "Which of the following is the strongest password?",
        "In general, the use of a passphrase is considered a best practice. Which of the following is the strongest example of a passphrase?",
        "By following best practices, what is the best way to create and store passwords?",
        "It is generally a good strategy to reuse passwords across multiple sites.",
        "\"____\" is the study of secure communications techniques that allow only the sender and intended recipient of a message to view its contents",
        "Can you decode this?",
        "Which of the following is not an aspect of multi-factor authentication?",
        "In order to login to an account, you need a fingerprint and a password. This is an example of:",
        "Choose the best answer. What should you do if a data breach happens on a website you use?",
        "Is it ethical behavior to rely on transitive trust and passwords?"
    };

    List<List<string>> level1questionChoiceList = new List<List<string>>()
    {
        /*new List<string> { // IGNORE FIRST QUESTION
            "Uppercase letters",
            "Lowercase letters",
            "Special characters",
            "Numbers",
            "Spaces"},  //1 */
        
        new List<string> {
            "password",
            "Admin123",
            "p@ssw0rd!",
            "p@sSw0rd!"},  //2
        
        new List<string> {
            "Admin123",
            "P@ssw0rd!",
            "Qwerty123!",
            "i<3C$4L!fe"},  //3
        
        new List<string> {
            "Sticky Note",
            "Excel",
            "Password Manager",
            "In your head"},  //4

        new List<string> {
            "True",
            "False"},  //5

        new List<string> {
            "Encryption",
            "Cryptography",
            "Blockchain",
            "Firewall"},  //6

        new List<string> {
            "A RED CAT",
            "A PET CAT",
            "I CAN WIN",
            "I PET DOG"},  //7

        new List<string> {
            "Something you know",
            "Something you are",
            "Something you read",
            "Something you have"},  //8

        new List<string> {
            "Single-factor authentication",
            "Two-factor authentication",
            "Three-factor authentication",
            "None of the above"},  //9

        new List<string> {
            "Do nothing. You’re probably safe",
            "Change the password on that specific site",
            "Change the password on the breached site along with any other site that uses the breached password"},  //10

        new List<string> {
            "True",
            "False"},  //11
    };

    List<string> level1questionAnswerList = new List<string>()
    {
        //"Uppercase letters",  //1
        "p@sSw0rd!",  //2
        "i<3C$4L!fe",  //3
        "Password Manager",  //4
        "False", //5
        "Cryptography", //6
        "A PET CAT", //7
        "Something you read", //8
        "Two-factor authentication", //9
        "Change the password on the breached site along with any other site that uses the breached password", //10
        "False" //11
    };

    void Start()
    {
        button1 = GameObject.Find("[Button] Button 1").GetComponent<Button>();
        button2 = GameObject.Find("[Button] Button 2").GetComponent<Button>();
        button3 = GameObject.Find("[Button] Button 3").GetComponent<Button>();
        button4 = GameObject.Find("[Button] Button 4").GetComponent<Button>();
        button5 = GameObject.Find("[Button] Button 5").GetComponent<Button>();

        UpdateDisplay();
        nextButton = GameObject.Find("[Button] Next").GetComponent<Button>();
        nextButton.interactable = false;
    }

    void UpdateDisplay()
    {
        ResetButtonColor();

        button1.gameObject.SetActive(true);
        button2.gameObject.SetActive(true);
        button3.gameObject.SetActive(true);
        button4.gameObject.SetActive(true);
        button5.gameObject.SetActive(true);

        questionNumber = GameObject.Find("[Text] Question Number").GetComponent<Text>();
        question = GameObject.Find("[Text] Question").GetComponent<Text>();
        option1 = GameObject.Find("[Text] Option 1").GetComponent<Text>();
        option2 = GameObject.Find("[Text] Option 2").GetComponent<Text>();
        option3 = GameObject.Find("[Text] Option 3").GetComponent<Text>();
        option4 = GameObject.Find("[Text] Option 4").GetComponent<Text>();
        option5 = GameObject.Find("[Text] Option 5").GetComponent<Text>();

        button1 = GameObject.Find("[Button] Button 1").GetComponent<Button>();
        button2 = GameObject.Find("[Button] Button 2").GetComponent<Button>();
        button3 = GameObject.Find("[Button] Button 3").GetComponent<Button>();
        button4 = GameObject.Find("[Button] Button 4").GetComponent<Button>();
        button5 = GameObject.Find("[Button] Button 5").GetComponent<Button>();


        List<Text> optionsList = new List<Text>() { option1, option2, option3, option4, option5 };
        List<Button> buttonList = new List<Button>() { button1, button2, button3, button4, button5 };

        List<string> thisQuestion = level1questionChoiceList[currQuestion - 2];

        questionNumber.text = "Question " + level1questionNumberList[currQuestion - 2];
        question.text = level1questionList[currQuestion - 2];

        int i = 0;

        while (i < 5)
        {
            if ( i < thisQuestion.Count)
            {
                optionsList[i].text = (i + 1) + ") " + thisQuestion[i];
            }
            else
            {
                optionsList[i].text = "";
                buttonList[i].gameObject.SetActive(false);
            }

            i++;
        }

        if (currQuestion == 1)
        {
            Debug.Log("All Options are used in question 1");
        }
        else if (currQuestion == 2)
        {
            button5.gameObject.SetActive(false);
        }
        else if (currQuestion == 3)
        {
            button5.gameObject.SetActive(false);
        }
        else if (currQuestion == 4)
        {
            button5.gameObject.SetActive(false);
        }
        else if (currQuestion == 5)
        {
            button3.gameObject.SetActive(false);
            button4.gameObject.SetActive(false);
            button5.gameObject.SetActive(false);
        }
        else if (currQuestion == 6)
        {
            button5.gameObject.SetActive(false);
        }
        else if (currQuestion == 7)
        {
            button5.gameObject.SetActive(false);
        }
        else if (currQuestion == 8)
        {
            button5.gameObject.SetActive(false);
        }
        else if (currQuestion == 9)
        {
            button5.gameObject.SetActive(false);
        }
        else if (currQuestion == 10)
        {
            button4.gameObject.SetActive(false);
            button5.gameObject.SetActive(false);
        }
        else if (currQuestion == 11)
        {
            button3.gameObject.SetActive(false);
            button4.gameObject.SetActive(false);
            button5.gameObject.SetActive(false);
        }

        if (currQuestion == 7)
        {
            caesarCipherImage.gameObject.SetActive(true);
        }
        else
        {
            caesarCipherImage.gameObject.SetActive(false);
        }
    }

    public void CheckAnswer(Button buttonObj)
    {
        string rightAnswer = level1questionAnswerList[currQuestion - 2];

        Text buttonText = buttonObj.GetComponentInChildren<Text>();
        string userChoiceStr = buttonText.text;
        userChoiceStr = userChoiceStr.Substring(3);

        thisButton = buttonObj;
        ColorBlock cb = thisButton.colors;

        if ( userChoiceStr.Equals(rightAnswer) )
        {
            if (firstTry)
            {
                level1UserCorrectness[currQuestion - 2] = true;
            }
            else
            {
                firstTry = true;
            }

            buttonObj.GetComponent<Image>().color = Color.green;

            nextButton.interactable = true;
        }
        else
        {
            firstTry = false;
            buttonObj.GetComponent<Image>().color = Color.red;
        }
    }

    public void GoNext()
    {
        ResetButtonColor();
        Invoke(nameof(SwitchQuestion), 0f);
        nextButton.interactable = false;
    }

    void SwitchQuestion()
    {
        Debug.Log("CURR QUESTION: " + currQuestion);

        if (currQuestion < 11)
        {
            currQuestion++;
            Invoke(nameof(UpdateDisplay), .5f);
        }
        else if (currQuestion == 11)
        {
            int correctCount = 0;

            foreach (bool b in level1UserCorrectness)
            {
                if (b)
                {
                    correctCount++;
                }
            }

            question.text = "YOU HAVE ANSWERED ( " + correctCount + " ) OUT OF ( " + level1UserCorrectness.Count + " ) CORRECT ON THE FIRST TRY.";

            questionNumber.text = " ";
            option1.text = " ";
            option2.text = " ";

            button1.gameObject.SetActive(false);
            button2.gameObject.SetActive(false);
            button3.gameObject.SetActive(false);
            button4.gameObject.SetActive(false);
            button5.gameObject.SetActive(false);

            currLevel++;
            currQuestion++;
            nextButton.interactable = true;
            //currQuestion = 1;
        }
        else
        {
            anim.SetBool("MinigameWon", true);
            Invoke("DelayedAction", delayTime);
        }
    }

    void DelayedAction()
    {
        Debug.Log("Waiting for " + delayTime + " Seconds till next task.");
        SceneManager.LoadScene(SceneToLoad);
    }

    void ResetButtonColor()
    {
        button1.GetComponent<Image>().color = Color.white;
        button2.GetComponent<Image>().color = Color.white;
        button3.GetComponent<Image>().color = Color.white;
        button4.GetComponent<Image>().color = Color.white;
        button5.GetComponent<Image>().color = Color.white;
    }

}