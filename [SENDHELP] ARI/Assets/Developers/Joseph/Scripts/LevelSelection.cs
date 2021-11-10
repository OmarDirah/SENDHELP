using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    public Button[] lvlButtons;
    public static List<int> levelListDone = new List<int>();
    public int[] completeLevels = new int[3];

    void Start()
    {
        int levelAt = PlayerPrefs.GetInt("levelAt", 2);

        Debug.Log("COMPLETED LEVELS BEGINNING");
        foreach (int i in completeLevels)
        {
            Debug.Log(i);
        }
        Debug.Log("COMPLETED LEVELS END");


        Debug.Log("COMPLETED LEVELS BEGINNING V2");
        foreach (int j in levelListDone)
        {
            Debug.Log(j);
        }
        Debug.Log("COMPLETED LEVELS END V2");


        for (int i = 0; i < lvlButtons.Length; i++)
        {
            if ( (i + 2 > levelAt) && (!(levelListDone.Contains(i))) )
                lvlButtons[i].interactable = false;
        }
    }

    public void AddCompletedLevels(int levelToAdd)
    {
        levelListDone.Add(levelToAdd);
    } 

}
