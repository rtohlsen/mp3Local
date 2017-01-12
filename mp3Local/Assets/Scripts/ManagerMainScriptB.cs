using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;

public class ManagerMainScriptB : MonoBehaviour {
    AudioClip myDownloadedClip;
    AudioClip myDiskLoadedClip;
    AudioSource myAudioSource;
    string myPath;
    public GameObject myDebugTextObject;


    void Start () {
        myAudioSource = GetComponent<AudioSource>();
        myPath = Application.persistentDataPath + "/testClip.xml";
        StartCoroutine(loadAudioFile());
    }

    IEnumerator loadAudioFile() {
        showDebugMessage("Downloading...");
        WWW www = new WWW("http://conservativestream.com/recorded-shows/Savage_01-09-2017_WCB_FULL.mp3");
        yield return www;
        myDownloadedClip = www.audioClip;
        // Now serialize and save audioclip
        bool didSave = UnityXMLSerializer.SerializeToXMLFile<AudioClip>(myPath, myDownloadedClip, true);
        if (didSave) {
            showDebugMessage("Saved as serialized AudioClip");
        } else {
            showDebugMessage("Failed to save as serialized AudioClip");
        }
    }

    public void loadClipFromDisk() {
        myDiskLoadedClip = UnityXMLSerializer.DeserializeFromXMLFile<AudioClip>(myPath);
        if (myDiskLoadedClip == null) {
            showDebugMessage("Failed to load AudioClip from disk");
        } else {
            showDebugMessage("Completed load of AudioClip from disk");
        }
    }

    public void playDiskAudioClip() {
        myAudioSource.clip = myDiskLoadedClip;
        myAudioSource.Play();
    }

    void showDebugMessage(string message) {
        myDebugTextObject.GetComponent<Text>().text = message;
    }
}


