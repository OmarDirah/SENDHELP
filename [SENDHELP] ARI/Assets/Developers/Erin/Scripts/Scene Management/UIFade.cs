using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFade : MonoBehaviour
{
    // VARIABLES
    public Animator anim;

    // These methods will be triggered by events from other scripts
    public void FadeToBlack()
    {
        anim.SetBool("MinigameWon", true);
    }

    public void FadeFromBlack()
    {
        anim.SetBool("MinigameWon", false);
    }
}
