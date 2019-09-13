using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CryingKidAnimate : MonoBehaviour {
    public static CryingKidAnimate instance;

    public GameObject theKid;

    public Animator cryingAnimation;
    public AudioSource soundCrying;
    bool firstTime = true;


    // Use this for initialization
    void Start () {
        instance = this;

        cryingAnimation = theKid.GetComponent<Animator>();
        soundCrying = theKid.GetComponent<AudioSource>();
    }


    private void OnTriggerEnter(Collider col)
    {
        //cryingAnimation.Play("Crying",0,0);
        // soundCrying.Play();
       // Debug.Log("OnTriggerEnter: The Timer is run");

        GameLogic.instance.timerPanel.SetActive(true);
        GameLogic.instance.theTimingProcessIsRunning = true;

        if (firstTime) {
            firstTime = false;
        //10 minutes is 600 seconds
        InvokeRepeating("Cry", 0, 30);
        }
    }

    void Cry()
    {
        //Debug.Log("30 seconds have passed");
        cryingAnimation.Play("Crying", 0, 0);
        soundCrying.Play();
    }
}
