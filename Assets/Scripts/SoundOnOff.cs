using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundOnOff : MonoBehaviour
{
    public SpriteRenderer rendererItem;
    public Sprite soundOn, soundOff;
    public AudioSource audio1, audio2;
    public bool sound = false;

    private void OnDisable()
    {
        if (sound)
        {
            PlayerPrefs.SetInt("volume", 1);
        }
        else
        {
            PlayerPrefs.SetInt("volume", 0);
        }
    }

    public void OnMouseDown()
    {
        if (sound)
        {
            rendererItem.sprite = soundOff;
            audio1.volume = 0;
            audio2.volume = 0;
            sound = false;
        }
        else
        {           
            rendererItem.sprite = soundOn;
            audio1.volume = 1f;
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                audio2.volume = 0.75f;
            }
            else
            {
                audio2.volume = 0.25f;
            }
            sound = true;
        }
    }
}
