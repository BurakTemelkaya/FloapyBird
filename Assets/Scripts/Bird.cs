using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Bird : MonoBehaviour
{
    public bool isDead;

    public float velocity = 1f;

    public int saniye, dakika, Heal;

    public Rigidbody2D rb2D;

    public GameManager managerGame;

    public GameObject DeathScreen, GameScren, admob;

    public AudioClip Point;

    public AudioSource Sounds, DeadScreenSound;

    public Animator anim;

    

    public Text TimeText, DeadTimeText, DeadSceneHeal;

    private int BirdSDeger;

    private float volume,dikeyHiz,yatayHiz=2.5f;

    private void Start()
    {
        volume = PlayerPrefs.GetFloat("Volume");
        Sounds.volume = volume;
        DeadScreenSound.volume = volume;

        BirdSDeger = PlayerPrefs.GetInt("SkinDegeri");
        if (BirdSDeger == 1)
        { anim.Play("RedBird"); }
        else if (BirdSDeger == 2)
        { anim.Play("BlueBird"); }
        else
        { anim.Play("YellowBird"); }

        Sounds.clip = Point;

        StartCoroutine(IEZaman());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            rb2D.velocity = Vector2.up * velocity;
            dikeyHiz = 1f;
        }

        dikeyHiz -= 3f * Time.deltaTime;

        float egim = 90 * dikeyHiz / yatayHiz;

        if (egim < -30)
            egim = -30;

        else if (egim > 30)
            egim = 30;

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, egim);
    }
    private void Times()
    {
        saniye++;  
        if (saniye == 60)
        {
            dakika++;
            saniye = 0;
        }
        if (dakika == 0)
        {
            TimeText.text = saniye.ToString();
        }
        else
        {
            if (dakika < 10)
            {
                if (saniye < 10)
                {
                    TimeText.text = "0" + dakika + ":" + "0" + saniye;
                }
                else
                {
                    TimeText.text = "0" + dakika + ":" + saniye;
                }
            }
            else
            {
                if (saniye < 10)
                {
                    TimeText.text = dakika + ":" + "0" + saniye;
                }
                else
                {
                    TimeText.text = dakika + ":" + saniye;
                }
            }
        }
    }
    public IEnumerator IEZaman()
    {        
        while(!isDead)
        {
            Times();
            yield return new WaitForSeconds(1f);
        }       
    }
    private void TimeSetting()
    {
        int HighTimeSaniye = PlayerPrefs.GetInt("Saniye");
        int HighTimeDakika = PlayerPrefs.GetInt("Dakika");

        if (HighTimeSaniye < saniye && HighTimeDakika <= dakika || HighTimeSaniye <= saniye && dakika < HighTimeDakika)
        {
            PlayerPrefs.SetString("HighZaman", TimeText.text);
                
            PlayerPrefs.SetInt("Saniye", saniye);
            PlayerPrefs.SetInt("Dakika", dakika);
        }
        DeadTimeText.text = TimeText.text;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "ScoreArea")
        {           
            Sounds.Play();
            managerGame.UpdateScore();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {     
        if (collision.gameObject.tag=="DeathArea")
        {
            TimeSetting();
            managerGame.HighScoreControl();
            managerGame.HealUpdate();
            Time.timeScale = 0;
            isDead = true;
            GameScren.SetActive(false);
            DeathScreen.SetActive(true);           
        }
    }

}
