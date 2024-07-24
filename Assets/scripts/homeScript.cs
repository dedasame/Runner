using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class homeScript : MonoBehaviour
{
    

    [SerializeField] public Text sumCoinText;
    [SerializeField] public Text highestScoreText;

    [SerializeField] public Button startBut;
    [SerializeField] public Button settBut;
    [SerializeField] public Button quitBut;

    [SerializeField] public AudioSource backgroundSound;
    [SerializeField] public AudioSource buttonSound;

    [SerializeField] public Button musicBut;
    [SerializeField] public Button soundBut;
    [SerializeField] public Button backBut;


    public int soundActive = 1;
    public int musicActive = 1;

    void Start()
    {

        sumCoinText.text = "COIN: " + PlayerPrefs.GetFloat("sumCoin");
        highestScoreText.text = "HIGH SCORE: " + PlayerPrefs.GetFloat("highestRoad").ToString("F2");


        soundActive = PlayerPrefs.GetInt("soundActive");
        musicActive = PlayerPrefs.GetInt("musicActive");

        // GetComponent<AudioSource>().Play();

        if (musicActive == 1 && !Controller.Instance)
        {
            backgroundSound.Play();
        }
        else
        {
            backgroundSound.Stop();
        }

        
       
        
        

    }

    
    void Update()
    {
    }


    public void start() //oyunu baþlatsin
    {
        
        if (soundActive == 1)
        {
            buttonSound.Play(); //ses kontrol
        }
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void settings() //?
    {
        
        if (soundActive == 1)
        {
            buttonSound.Play(); //ses kontrol
        }
        
        startBut.gameObject.SetActive(false);
        settBut.gameObject.SetActive(false);
        quitBut.gameObject.SetActive(false);

        musicBut.gameObject.SetActive(true);
        soundBut.gameObject.SetActive(true);

        backBut.gameObject.SetActive(true);
    }

    public void quit() //oyundan cik
    {
        
        if (soundActive == 1)
        {
            buttonSound.Play(); //ses kontrol
        }
        
        Application.Quit();
    }

    public void MusicFunc()
    {
        
        musicActive = PlayerPrefs.GetInt("musicActive");

        if (musicActive == 1)
        {
            PlayerPrefs.SetInt("musicActive", 0);
            musicActive = 0;
            backgroundSound.Stop();
        }
        else
        {
            PlayerPrefs.SetInt("musicActive", 1);
            musicActive = 1;
            backgroundSound.Play();
        }
        
        
    }

    public void SoundFunc()
    {
        
        soundActive = PlayerPrefs.GetInt("soundActive");

        if (soundActive == 1)
        {
            buttonSound.Play(); //ses kontrol
        }


        if (soundActive == 1) 
        {
            PlayerPrefs.SetInt("soundActive", 0);
            soundActive = 0;
        }
        else
        {
            PlayerPrefs.SetInt("soundActive", 1);
            soundActive = 1;
        }
        
    }

    public void BackFunc()
    {

        if (soundActive == 1)
        {
            buttonSound.Play(); //ses kontrol
        }

        startBut.gameObject.SetActive(true);
        settBut.gameObject.SetActive(true);
        quitBut.gameObject.SetActive(true);

        musicBut.gameObject.SetActive(false);
        soundBut.gameObject.SetActive(false);

        backBut.gameObject.SetActive(false);

    }

}
