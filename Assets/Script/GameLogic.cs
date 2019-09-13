using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class GameLogic : MonoBehaviour {
    public static GameLogic instance; //Do GameLogic as a Singlton class
    private Player player;
    public GameObject timerPanel;
    public Text timeText;
    public GameObject gameOverPanel;
    public GameObject winningPanel;
    public GameObject theToy;

    public bool theTimingProcessIsRunning = false;


    float targetTime = 180; // how long you want to wait in seconds (i.e 5 seconds)
    float minutes, seconds = 0;
    string minutesString, secondsString = "";


    private Coroutine btnAndTxtHintCoroutine;
    public SteamVR_Action_Boolean action = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("Grab Pinch");
    bool firstTime = true;


    // Use this for initialization
    void Start () {
        instance = this;
        player = Player.instance;
        //coroutine = WaitTime(1);
        StartCoroutine(WaitTime(1));
    }

    // Update is called once per frame
    void Update () {

        if (theTimingProcessIsRunning)
        {
            //##################################################  Code for Gaping Hint ##################################################
            if (firstTime)
            {
                firstTime = false;
                DisableHint();

                Invoke("ShowHint", 1.0f);

            }
            if (SteamVR_Actions.default_GrabPinch.GetStateDown(SteamVR_Input_Sources.RightHand))//actionDeleteMenuTool.GetStateDown(SteamVR_Input_Sources.LeftHand)) //Release
            {
                DisableHint();
            }

            //#########################################################################################################################

            WaitTime(1);
            targetTime -= Time.deltaTime;

            if (targetTime < 0)
            {
                // we have reched the time
                instance.gameOverPanel.SetActive(true);

                StartCoroutine(WaitTimeSeconds(5));
                //WaitTimeSeconds(5);
               // WaitTime(5);
 
            }
            else { 

            // minutes = targetTime / 60;
            minutes = Mathf.Floor(targetTime / 60);
            // seconds = targetTime % 60;
            seconds = Mathf.RoundToInt(targetTime % 60);

            if (minutes < 10)
            {
                minutesString = "0" + minutes.ToString();
            }
            else
            {
                minutesString = minutes.ToString();
            }
            if (seconds < 10)
            {
                secondsString = "0" + seconds.ToString();//Mathf.RoundToInt(seconds).ToString();
            }
            else
            {
                secondsString = seconds.ToString();
            }

            timeText.text = minutesString + ":" + secondsString;
            }

        }
    }

    IEnumerator WaitTime(int sec)
    {
        //if (sec == 5) { Debug.Log("WaitTime (5) "); }
        yield return new WaitForSeconds(sec);
    }
    
    IEnumerator WaitTimeSeconds(int sec)
    {
        yield return new WaitForSeconds(sec);
        //StopCoroutine("WaitTime");
        Reset();
    }

    void Reset()
    {
        // do resert logic if needed

        theTimingProcessIsRunning = false;
        targetTime = 180;
        instance.gameOverPanel.SetActive(false);
        player.transform.position = new Vector3(-52.6f, 0.45f, -91.35f);
        theToy.transform.position = new Vector3(-19f, 0f, -116.111f);
        instance.timerPanel.SetActive(false);
        EnteringTheStore.instance.videoToysRUs.Stop();
        EnteringTheStore.instance.soundVideoToysRUS.Stop();

        CancelInvoke("Cry"); //To stop Animator

        StopCoroutine("WaitTimeSeconds");
        
    }


    //##########################################################################################################
    public void ShowHint()
    {
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
        if (btnAndTxtHintCoroutine != null)
        {
            ControllerButtonHints.HideTextHint(player.rightHand, action);

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
