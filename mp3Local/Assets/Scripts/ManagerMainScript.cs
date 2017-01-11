using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System;

public class ManagerMainScript : MonoBehaviour {
    AudioClip myClip;
    AudioClip myClipFromDisk;
    AudioSource myAudioSource;
    WWW diskWWW;
    public GameObject myDebugTextObject;

	void Start () {
        myAudioSource = GetComponent<AudioSource>();
        //loadPreDownloadedFile();
        StartCoroutine(loadAudioFile());
    }

    IEnumerator loadAudioFile() {
        showDebugMessage("Downloading...");
        WWW www = new WWW("http://conservativestream.com/recorded-shows/Savage_01-09-2017_WCB_FULL.mp3");
        yield return www;
        myClip = www.audioClip;
        showDebugMessage("Download over");

        string filePath = Path.Combine(Application.persistentDataPath, "testFile.mp3");
        showDebugMessage("Writing File");
        System.IO.File.WriteAllBytes(filePath, www.bytes);

        StartCoroutine(LoadAudioFromDisk("file://" + filePath));
    }

    void loadPreDownloadedFile() {
        string filePath = Path.Combine(Application.persistentDataPath, "testFile.mp3");
        StartCoroutine(LoadAudioFromDisk("file://" + filePath));
    }

    IEnumerator LoadAudioFromDisk(string filePath) {
        //byte[] levelData = System.IO.File.ReadAllBytes(filePath);//full local save file
        //float[] f = ConvertByteToFloat(levelData);
        ////myClipFromDisk = AudioClip.Create("WebTestSound", f.Length, 1, 44100, true);
        //myClipFromDisk = AudioClip.Create("WebTestSound2", f.Length, 1, 44100, true);
        //myClipFromDisk.GetData(f, 0);
        //Debug.Log("Load from disk over");

        showDebugMessage("Loading www from disk");
        diskWWW = new WWW(filePath);
        //AudioClip tempAudioClip = www.audioClip;
        //while (tempAudioClip.loadState == AudioDataLoadState.Loading)
        yield return diskWWW;

        //showDebugMessage("extracting audio from www object");
        StartCoroutine(waitThenPlayDiskClip());

        // next line temporary to make sure I can play a loaded file
        //StartCoroutine(waitThenPlayDownloadedClip());

        //myClipFromDisk = www.audioClip;
        //playDiskAudio();
    }

    // This is temporary, plays the clip downloaded directly
    IEnumerator waitThenPlayDownloadedClip() {
        showDebugMessage("Aattaching clip to audio in 3 sec");
        yield return new WaitForSeconds(3.0f);
        myAudioSource.clip = myClip;
        showDebugMessage("Playing audio");
        myAudioSource.Play();
    }

    // This is temporary, plays the disk loaded clip after waiting 10 seconds
    IEnumerator waitThenPlayDiskClip() {
        AudioClip clip = diskWWW.GetAudioClip(false, false);
        showDebugMessage("Aattaching clip to audio in 10 sec");
        yield return new WaitForSeconds(10.0f);
        myAudioSource.clip = clip;
        showDebugMessage("Playing audio");
        myAudioSource.Play();
    }

    void playDiskAudio() {
        showDebugMessage("Aattaching clip to audio");
        myAudioSource.clip = myClipFromDisk;
        showDebugMessage("Playing audio");
        myAudioSource.Play();
        //myAudioSource.PlayOneShot(myClipFromDisk);
    }
    //void playTestClip() {
    //    Debug.Log("attaching clip to audio");
    //    myAudioSource.clip = TestClip;
    //    myAudioSource.Play();
    //    //myAudioSource.PlayOneShot(myClipFromDisk);
    //    Debug.Log("Playing TestClip audio");
    //}


    private float[] ConvertByteToFloat(byte[] array) {
        float[] floatArr = new float[array.Length / 4];
        for (int i = 0; i < floatArr.Length; i++) {
            if (BitConverter.IsLittleEndian)
                Array.Reverse(array, i * 4, 4);
            floatArr[i] = BitConverter.ToSingle(array, i * 4);
        }
        return floatArr;
    }

    void showDebugMessage(string message) {
        myDebugTextObject.GetComponent<Text>().text = message;
    }

}
