using UnityEngine;
using System.Collections;

public class GazeController : MonoBehaviour {

    //Create array for video library
    public MovieTexture[] VideoLibrary;

    //Creates video index
    private int _videoIndex;
    //Create controller states
    private enum _controllerOption{PLAY, PAUSE, NEXT, LAST };
    private _controllerOption _options;

    //Create timer for timed gazing
    private int timer;

    //Create button game objects
    GameObject PlayButton;
    GameObject PauseButton;
    GameObject ForwardButton;
    GameObject BackButton;

	// Initialize everything
	void Start () {
        //sync buttons
        PlayButton = GameObject.Find("Play");
        PauseButton = GameObject.Find("Pause");
        ForwardButton = GameObject.Find("ForwardRight");
        BackButton = GameObject.Find("ForwardLeft");

        //initialize timer
        timer = 0;

        //intialize video index;
        _videoIndex = 0;
    }
	
	// Update is called once per frame
	void Update () {
        //Get the forward pointing Ray from the camera up to 5 units out
        Vector3 fwd = transform.TransformDirection(Vector3.forward) * 5;
        RaycastHit hit;

        //If the camera is looking at a button do something
        if(Physics.Raycast(transform.position,fwd,out hit))
        {
            switch (hit.collider.tag)
            {
                case "Play":
                    timer++;
                    ControllerAction(_controllerOption.PLAY);
                    break;
                case "Pause":
                    timer++;
                    ControllerAction(_controllerOption.PAUSE);
                    break;
                case "Next":
                    timer++;
                    ControllerAction(_controllerOption.NEXT);
                    break;
                case "Last":
                    timer++;
                    ControllerAction(_controllerOption.LAST);
                    break;
                default:
                    timer = 0;
                    break;
            }
        }
	
	}

    void ControllerAction(_controllerOption option)
    {
        switch(timer/120)
        {
            case 1://do nothing for the first second
                break;
            case 2://take action
                switch(option)
                {
                    case _controllerOption.PLAY:
                        if(!GameObject.Find("default").GetComponent<VideoPlayer>().movTexture.isPlaying)
                        {
                            GameObject.Find("default").GetComponent<VideoPlayer>().movTexture.Play();
                            GameObject.Find("default").GetComponent<VideoPlayer>().movAudio.Play();
                        }
                        break;
                    case _controllerOption.PAUSE:
                        if (GameObject.Find("default").GetComponent<VideoPlayer>().movTexture.isPlaying)
                        {
                            GameObject.Find("default").GetComponent<VideoPlayer>().movTexture.Pause();
                            GameObject.Find("default").GetComponent<VideoPlayer>().movAudio.Pause();
                        }
                        break;
                    case _controllerOption.LAST:
                        _videoIndex--;
                        if(_videoIndex<0)
                        {
                            _videoIndex = VideoLibrary.Length - 1;
                        }
                        PlayVideo(_videoIndex);
                        break;
                    case _controllerOption.NEXT:
                        _videoIndex++;
                            if(_videoIndex>VideoLibrary.Length)
                        {
                            _videoIndex = 0;
                        }
                        PlayVideo(_videoIndex);
                        break;
                    default:
                        break;
                }
                break;
        }
    }

    void PlayVideo(int index)
    {
        GameObject.Find("default").GetComponent<Renderer>().material.mainTexture = VideoLibrary[index];
        GameObject.Find("default").GetComponent<VideoPlayer>().movTexture = VideoLibrary[index];
        GameObject.Find("default").GetComponent<VideoPlayer>().movAudio = GameObject.Find("default").GetComponent<AudioSource>();
        GameObject.Find("default").GetComponent<VideoPlayer>().movAudio.clip = GameObject.Find("default").GetComponent<VideoPlayer>().movTexture.audioClip;
        GameObject.Find("default").GetComponent<VideoPlayer>().movTexture.Play();
        GameObject.Find("default").GetComponent<VideoPlayer>().movAudio.Play();
    }
}
