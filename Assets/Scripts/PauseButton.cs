using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour {

    private bool pause;
    private bool mute;
    private Sprite[] muteImage;
    private Sprite[] pauseImage;

    public Button muteButton;
    public Button pauseButton;
    public Canvas canvas;

	void Start () {
        canvas.GetComponent<Canvas>().enabled = false;
        pause = false;
        mute = true;
        muteImage = Resources.LoadAll<Sprite>("Mute");
        pauseImage = Resources.LoadAll<Sprite>("pause");
        muteButton.image.sprite = muteImage[0];
        pauseButton.image.sprite = pauseImage[0];
        
	}
	
	void Update () {

    }

    public void ChangePause()
    {
         pause = !pause;
        if (pause)
        {
            Time.timeScale = 0;
            pauseButton.image.sprite = pauseImage[1];
            canvas.GetComponent<Canvas>().enabled = true;
        }
        else
        {
            Time.timeScale = 1;
            pauseButton.image.sprite = pauseImage[0];
            canvas.GetComponent<Canvas>().enabled = false;
        }
    }

    public void ChangeMute()
    {
        mute = !mute;
        if (mute)
        {
            AudioListener.volume = 1;
            muteButton.image.sprite = muteImage[0];
        }
        else
        {
            AudioListener.volume = 0;
            muteButton.image.sprite = muteImage[1];
        }
    }

}
