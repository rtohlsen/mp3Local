using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System;

public class ManagerMainScriptB : MonoBehaviour {
    AudioClip myClip;
    AudioSource myAudioSource;
    public GameObject myDebugTextObject;


    void Start () {
        myAudioSource = GetComponent<AudioSource>();
        StartCoroutine(loadAudioFile());
    }

    IEnumerator loadAudioFile() {
        showDebugMessage("Downloading...");
        WWW www = new WWW("http://conservativestream.com/recorded-shows/Savage_01-09-2017_WCB_FULL.mp3");
        yield return www;
        myClip = www.audioClip;
        // Now save myself as prefab
        
    }

    void showDebugMessage(string message) {
        myDebugTextObject.GetComponent<Text>().text = message;
    }
}
