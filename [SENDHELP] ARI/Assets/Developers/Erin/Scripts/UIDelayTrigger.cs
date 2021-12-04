using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDelayTrigger : MonoBehaviour
{
    // VARIABLES
    public Animator anim;

    public void CueCredits()
    {
        anim.SetBool("CreditScroll", true);
    }

    public void ResetCredits()
    {
        anim.SetBool("CreditScroll", false);
    }
}
