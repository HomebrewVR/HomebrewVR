using UnityEngine;
using System.Collections;

public class VideoPlayer : MonoBehaviour {
    public MovieTexture movTexture;
    public AudioSource movAudio;
    // Use this for initialization
    void Start () {
        GetComponent<Renderer>().material.mainTexture = movTexture;
        movAudio = GetComponent<AudioSource>();
        movAudio.clip = movTexture.audioClip;
        movTexture.Play();
        movAudio.Play();
    }
	
	// Update is called once per frame
	void Update () {
	}
}