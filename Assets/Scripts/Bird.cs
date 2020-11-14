﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Bird : MonoBehaviour
{
    public bool isDead=true;

    public float velocity = 1f;

    [HideInInspector] public int saniye, dakika, Heal;

    [HideInInspector] public Rigidbody2D rb2D;

    [HideInInspector] public GameManager managerGame;

    [HideInInspector]public GameObject DeathScreen, GameScren, admob, Nigh , Morning;

    [HideInInspector]public AudioClip Point;

    [HideInInspector] public AudioSource Sounds, DeadScreenSound;

    [HideInInspector] public Animator anim;

    [HideInInspector] public Text TimeText, DeadTimeText, DeadSceneHeal;

    [HideInInspector] private int BirdSDeger,wait;

    private float volume,dikeyHiz,yatayHiz=2.5f,egim;

    private void Start()
    {
        volume = PlayerPrefs.GetFloat("Volume");
        Sounds.volume = volume;
        DeadScreenSound.volume = volume;

        int gercekZaman = DateTime.Now.Hour;
        if (gercekZaman <= 6 && gercekZaman >= 18)
            Nigh.SetActive(true);
        else
            Morning.SetActive(true);

        BirdSDeger = PlayerPrefs.GetInt("SkinDegeri");
        if (BirdSDeger == 1)
        { anim.Play("RedBird"); }
        else if (BirdSDeger == 2)
        { anim.Play("BlueBird"); }
        else
        { anim.Play("YellowBird"); }

        Sounds.clip = Point;

        StartCoroutine(IEZaman());



        Time.timeScale = 1;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isDead)
        {
            rb2D.velocity = Vector2.up * velocity;
            dikeyHiz = 1.5f;
        }

        dikeyHiz -= 4.5f * Time.deltaTime;

        egim = 90 * dikeyHiz / yatayHiz;

        if (egim < -30)
            egim = -30;

        else if (egim > 30)
            egim = 30;

        transform.localEulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, egim);
    }

    public IEnumerator IEZaman()
    {
        while (!isDead)
        {
            Times();
            if (wait > 0)
            {
                wait--;
            }
            yield return new WaitForSeconds(1f);
            
        }
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
        if (collision.gameObject.name == "ScoreArea" && wait==0)
        {           
            Sounds.Play();
            managerGame.UpdateScore();
            wait = 2;
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
            managerGame.GecisReklamiKontrolu();
        }
    }

}
