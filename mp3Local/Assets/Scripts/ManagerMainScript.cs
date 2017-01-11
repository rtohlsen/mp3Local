using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class ManagerMainScript : MonoBehaviour {
    AudioClip myClip;
    AudioClip myClipFromDisk;
    AudioSource myAudioSource;

	void Start () {
        myAudioSource = GetComponent<AudioSource>();
        //loadPreDownloadedFile();
        StartCoroutine(loadAudioFile());
    }

    IEnumerator loadAudioFile() {
        WWW www = new WWW("http://conservativestream.com/recorded-shows/Savage_01-09-2017_WCB_FULL.mp3");
        yield return www;
        myClip = www.audioClip;
        Debug.Log("Download over");

        string filePath = Application.persistentDataPath + "/testFile.mp3";
        System.IO.File.WriteAllBytes(filePath, www.bytes);

        StartCoroutine(LoadAudioFromDisk("file://" + filePath));
    }

    void loadPreDownloadedFile() {
        string filePath = Application.persistentDataPath + "/testFile.mp3";
        StartCoroutine(LoadAudioFromDisk("file://" + filePath));
    }

    IEnumerator LoadAudioFromDisk(string filePath) {
        //byte[] levelData = System.IO.File.ReadAllBytes(filePath);//full local save file
        //float[] f = ConvertByteToFloat(levelData);
        ////myClipFromDisk = AudioClip.Create("WebTestSound", f.Length, 1, 44100, true);
        //myClipFromDisk = AudioClip.Create("WebTestSound2", f.Length, 1, 44100, true);
        //myClipFromDisk.GetData(f, 0);
        //Debug.Log("Load from disk over");

        WWW www = new WWW(filePath);
        yield return www;
        myClipFromDisk = www.audioClip;
        playDiskAudio();
    }

    void playDiskAudio() {
        Debug.Log("attaching clip to audio");
        myAudioSource.clip = myClipFromDisk;
        myAudioSource.Play();
        //myAudioSource.PlayOneShot(myClipFromDisk);
        Debug.Log("Playing audio");
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

}
