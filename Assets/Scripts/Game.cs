using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public GameObject player;
    public GameObject block1, block2, block3, block4;
    public Vector3 pos1, pos2, pos3, pos4;
    public GameObject text1, text2, text3, text4;
    public GameObject questionText, lifeCountText, scoreText;
    public GameObject gameOverPanel;
    public float num1, num2, num3, num4;
    public AudioSource audioSource;
    public AudioSource backgroundMusic;
    public AudioClip deathClip, scoreBar, clickClip;
    public int gameNum1, gameNum2;
    public float answer;
    public int lives = 3, score = 0;
    public float difficulty = 1;
    public int ballType = 1;
    public Sprite a, b, c;
    public bool scaleFont = false;
    public bool scaleFontRev = false;
    public bool scaleScore = false;
    public bool scaleScoreRev = false;
    public int highScore = 0;

    void OnEnable()
    {
        highScore = PlayerPrefs.GetInt("highScore");
        difficulty = PlayerPrefs.GetFloat("difficulty");
        ballType = PlayerPrefs.GetInt("ballType");
        if (PlayerPrefs.GetInt("volume") == 0)
        {
            FindObjectOfType<SoundOnOff>().sound = true;
            FindObjectOfType<SoundOnOff>().OnMouseDown();
        }
        else
        {
            FindObjectOfType<SoundOnOff>().sound = false;
            FindObjectOfType<SoundOnOff>().OnMouseDown();
        }
    }

    private void OnDisable()
    {
        if (score > highScore)
        {
            PlayerPrefs.SetInt("highScore", score);
        }      
    }

    private void Start()
    {
        Time.timeScale = 1;

        backgroundMusic.pitch = 1 + (difficulty - 1) / 10;

        if (ballType == 1)
        {
            player.GetComponent<SpriteRenderer>().sprite = a;
        }
        else if (ballType == 2)
        {
            player.GetComponent<SpriteRenderer>().sprite = b;
        }
        else if (ballType == 3)
        {
            player.GetComponent<SpriteRenderer>().sprite = c;
        }
      
        pos1 = block1.transform.position;
        pos2 = block2.transform.position;
        pos3 = block3.transform.position;
        pos4 = block4.transform.position;
        GenerateNumbers();
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            audioSource.PlayOneShot(clickClip);
        }

        if (block1.transform.position.y <= -3.5)
        {
            ResetGame();
        }

        block1.transform.Translate(0, -1 * Time.deltaTime * difficulty, 0);
        block2.transform.Translate(0, -1 * Time.deltaTime * difficulty, 0);
        block3.transform.Translate(0, -1 * Time.deltaTime * difficulty, 0);
        block4.transform.Translate(0, -1 * Time.deltaTime * difficulty, 0);
        text1.transform.position = block1.transform.position;
        text2.transform.position = block2.transform.position;
        text3.transform.position = block3.transform.position;
        text4.transform.position = block4.transform.position;

        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                SceneManager.LoadScene(0);

                return;
            }
        }

        float font = lifeCountText.GetComponent<TextMeshProUGUI>().fontSize;

        if (scaleFont)
        {
            if (font <= 45)
            {
                lifeCountText.GetComponent<TextMeshProUGUI>().fontSize += 0.3f;
            }
            else
            {
                scaleFont = false;
                scaleFontRev = true;
            }
        }
        else if (scaleFontRev)
        {
            if (font >= 36)
            {
                lifeCountText.GetComponent<TextMeshProUGUI>().fontSize -= 0.3f;
            }
            else
            {
                scaleFontRev = false;
            }
        }

        font = scoreText.GetComponent<TextMeshProUGUI>().fontSize;

        if (scaleScore)
        {
            if (font <= 45)
            {
                scoreText.GetComponent<TextMeshProUGUI>().fontSize += 0.3f;
            }
            else
            {
                scaleScore = false;
                scaleScoreRev = true;
            }
        }
        else if (scaleScoreRev)
        {
            if (font >= 36)
            {
                scoreText.GetComponent<TextMeshProUGUI>().fontSize -= 0.3f;
            }
            else
            {
                scaleScoreRev = false;
            }
        }
    }

    public void GenerateNumbers()
    {
        gameNum1 = Random.Range(1, 101);
        gameNum2 = Random.Range(1, 101);

        int operatorNum = Random.Range(0, 4);
        string operatorSign = string.Empty;

        if (operatorNum == 0)
        {
            operatorSign = " + ";
            answer = gameNum1 + gameNum2;
        }
        else if (operatorNum == 1)
        {
            operatorSign = " - ";
            answer = gameNum1 - gameNum2;
        }
        else if (operatorNum == 2)
        {
            operatorSign = " * ";
            answer = gameNum1 * gameNum2;
        }
        else if (operatorNum == 3)
        {
            operatorSign = " / ";
            answer = gameNum1 / gameNum2;
        }

        questionText.GetComponent<TextMeshProUGUI>().text = gameNum1.ToString() + operatorSign + gameNum2.ToString() + " = ?";

        int correctBlock = Random.Range(0, 4);
        
        if (correctBlock == 0)
        {
            num1 = answer;
            num2 = answer + Random.Range(-10, 11);
            num3 = answer + Random.Range(-10, 11);
            num4 = answer + Random.Range(-10, 11);
        }
        else if (correctBlock == 1)
        {
            num1 = answer + Random.Range(-10, 11);
            num2 = answer;
            num3 = answer + Random.Range(-10, 11);
            num4 = answer + Random.Range(-10, 11);
        }
        else if (correctBlock == 2)
        {
            num1 = answer + Random.Range(-10, 11);
            num2 = answer + Random.Range(-10, 11);
            num3 = answer;
            num4 = answer + Random.Range(-10, 11);
        }
        else if (correctBlock == 3)
        {
            num1 = answer + Random.Range(-10, 11);
            num2 = answer + Random.Range(-10, 11);
            num3 = answer + Random.Range(-10, 11);
            num4 = answer;
        }

        text1.GetComponent<TextMeshProUGUI>().text = num1.ToString();
        text2.GetComponent<TextMeshProUGUI>().text = num2.ToString();
        text3.GetComponent<TextMeshProUGUI>().text = num3.ToString();
        text4.GetComponent<TextMeshProUGUI>().text = num4.ToString();
    }

    public void CheckAnswer(GameObject block)
    {
        if (block == block1)
        {
            if (num1 == answer)
            {
                score++;
                scaleScore = true;
                scoreText.GetComponent<TextMeshProUGUI>().text = "Score: " + score;
                ResetGame();
            }
            else
            {
                score--;
                scaleScore = true;
                Damage();
                scoreText.GetComponent<TextMeshProUGUI>().text = "Score: " + score;
            }
        }
        else if (block == block2)
        {
            if (num2 == answer)
            {
                score++;
                scaleScore = true;
                scoreText.GetComponent<TextMeshProUGUI>().text = "Score: " + score;
                ResetGame();
            }
            else
            {
                score--;
                scaleScore = true;
                Damage();
                scoreText.GetComponent<TextMeshProUGUI>().text = "Score: " + score;
            }
        }
        else if (block == block3)
        {
            if (num3 == answer)
            {
                score++;
                scaleScore = true;
                scoreText.GetComponent<TextMeshProUGUI>().text = "Score: " + score;
                ResetGame();
            }
            else
            {
                score--;
                scaleScore = true;
                Damage();
                scoreText.GetComponent<TextMeshProUGUI>().text = "Score: " + score;
            }
        }
        else if (block == block4)
        {
            if (num4 == answer)
            {
                score++;
                scaleScore = true;
                scoreText.GetComponent<TextMeshProUGUI>().text = "Score: " + score;
                ResetGame();
            }
            else
            {
                score--;
                scaleScore = true;
                Damage();
                scoreText.GetComponent<TextMeshProUGUI>().text = "Score: " + score;
            }
        }

        if (score % 10 == 0)
        {
            audioSource.PlayOneShot(scoreBar);
        }
    }

    public void Damage()
    {
        audioSource.PlayOneShot(deathClip);
        lives--;
        scaleFont = true;
        lifeCountText.GetComponent<TextMeshProUGUI>().text = "Lives: " + lives;

        if (lives <= 0)
        {
            GameOver();
        }
    }

    public void ResetGame()
    {
        block1.transform.position = pos1;
        block2.transform.position = pos2;
        block3.transform.position = pos3;
        block4.transform.position = pos4;
        GenerateNumbers();
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
    }
}
