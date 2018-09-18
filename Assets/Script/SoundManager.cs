using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

    public static SoundManager instance;

    public AudioSource myAudio;
    
    public AudioClip go;

    // Use this for initialization
    void Start () {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void open()
    {
        myAudio.PlayOneShot(go);
    }
}
