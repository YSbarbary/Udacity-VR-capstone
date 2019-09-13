using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Winning : MonoBehaviour
{
    public GameObject theKid;
    public RuntimeAnimatorController jumpingAnimationController;
    public Avatar jumpingAvatar;
    public AudioClip clipLaughing;

    Animator kidAnimator;
    AudioSource soundLaughing;

    // Use this for initialization
    void Start()
    {
        kidAnimator = theKid.GetComponent<Animator>();
        soundLaughing = theKid.GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("TruckToy"))
        {
            GameLogic.instance.winningPanel.SetActive(true);
            GameLogic.instance.timerPanel.SetActive(false);
            GameLogic.instance.theTimingProcessIsRunning = false;

            kidAnimator.runtimeAnimatorController = jumpingAnimationController;
            kidAnimator.avatar = jumpingAvatar;
            soundLaughing.clip = clipLaughing;
            soundLaughing.Play();
            kidAnimator.Play("Jumping", 0, 0);
        }
    }
}
