using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject panel;
    public AudioSource audioSource;
    public AudioSource backgroundMusic; 
    public AudioClip clickClip;
    public bool active = false;
    public byte rComp = 0;
    public bool deloading = false;
    public float difficulty = 1;
    public int ballType = 1;

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    void OnDisable()
    {
        PlayerPrefs.SetFloat("difficulty", difficulty);
        PlayerPrefs.SetInt("ballType", ballType);
    }

    public void Update()
    {
        backgroundMusic.pitch = 1 + (difficulty - 1) / 10;

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            audioSource.PlayOneShot(clickClip);
        }

        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                if (active)
                {
                    DeLoadComponents();
                }
                else
                {
                    Application.Quit();
                }

                return;
            }
        }          

        if (active && rComp < 255)
        {
            rComp++;
            panel.GetComponent<Image>().color = new Color32(0, 0, 0, rComp);
        }

        if (!deloading && rComp >= 255)
        {
            LoadComponents();
        }

        if (deloading)
        {
            active = false;
            rComp--;
            panel.GetComponent<Image>().color = new Color32(0, 0, 0, rComp);
        }

        if (active && rComp >= 255)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject.name == "ball1")
                    {
                        ballType = 1;
                    }
                    else if (hit.collider.gameObject.name == "ball2")
                    {
                        ballType = 2;
                    }
                    else if (hit.collider.gameObject.name == "ball3")
                    {
                        ballType = 3;
                    }
                    FindObjectOfType<SelectBall>().Selected(hit.collider.gameObject.GetComponent<SpriteRenderer>());
                }
            }           

            difficulty = FindObjectOfType<Slider>().value;
        }

        if (rComp <= 0)
        {
            deloading = false;
            OptionsExit();
        }
    }

    public void LoadComponents()
    {
        for (int i = 0; i < panel.transform.childCount; i++)
        {
            panel.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void DeLoadComponents()
    {
        for (int i =0; i<panel.transform.childCount; i++)
        {
            panel.transform.GetChild(i).gameObject.SetActive(false);
        }
        deloading = true;
    }

    public void OptionsEnter()
    {
        panel.SetActive(true);
        active = true;
    }

    public void OptionsExit()
    {
        panel.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
