using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class GrapToy : MonoBehaviour {

    private Coroutine btnAndTxtHintCoroutine;
    private Player player = null;
    public SteamVR_Action_Boolean action = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("Grab Pinch");
    bool firstTime = true;


    void Start()
    {
        player = Player.instance;
    }


    void Update()
  {
        if (firstTime)
        {
            
            firstTime = false;
            DisableHint();

            Invoke("ShowHint", 1.0f);

        }
        if (SteamVR_Actions.default_GrabGrip.GetStateDown(SteamVR_Input_Sources.RightHand))//actionDeleteMenuTool.GetStateDown(SteamVR_Input_Sources.LeftHand)) //Release
        {
            DisableHint();
        }

    }

    //-------------------------------------------------
    public void ShowHint()
    {
        Debug.Log(" ShowHint  is invoked");
        DisableHint();
        if (btnAndTxtHintCoroutine != null)
        {
            StopCoroutine(btnAndTxtHintCoroutine);
        }
        btnAndTxtHintCoroutine = StartCoroutine(HintCoroutine());
    }

    //-------------------------------------------------
    public void DisableHint()
    {
        Debug.Log("Disable Hints is invoked");
        if (btnAndTxtHintCoroutine != null)
        {
            ControllerButtonHints.HideTextHint(player.leftHand, action);

            StopCoroutine(btnAndTxtHintCoroutine);
            btnAndTxtHintCoroutine = null;
        }

        CancelInvoke("ShowHint");
    }

    //-------------------------------------------------
    // Cycles through all the button hints on the controller
    //-------------------------------------------------
    private IEnumerator HintCoroutine()
    {
        Hand hand = player.rightHand;
        while (true)
        {
            if (action.GetActive(hand.handType))
            {
                ControllerButtonHints.ShowTextHint(hand, action, "Grab Toy");
            }
            else
            {
                ControllerButtonHints.HideButtonHint(hand, action);
            }

            yield return new WaitForSeconds(3.0f);

            yield return null;
        }
    }
}
