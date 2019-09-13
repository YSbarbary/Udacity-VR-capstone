using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Audio;

public class EnteringTheStore : MonoBehaviour
{
    public static EnteringTheStore instance;

    public VideoPlayer videoToysRUs;
    public AudioSource soundVideoToysRUS;
    public GameObject videoShow;

    // Use this for initialization
    void Start()
    {
        instance = this;

        videoToysRUs = videoShow.GetComponent<VideoPlayer>();
        soundVideoToysRUS = videoShow.GetComponent<AudioSource>();
    }


    private void OnTriggerEnter(Collider col)
    {
        //Debug.Log("OnTriggerEnter: The Video is run");
        videoToysRUs.Play();
        soundVideoToysRUS.Play();
    }
}
