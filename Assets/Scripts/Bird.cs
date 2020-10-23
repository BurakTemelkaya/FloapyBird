﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Bird : MonoBehaviour
{
    public bool isDead;

    public float velocity = 1f , Zaman;

    public int SaatDeger,dakika,LastTime,Heal;

    public Rigidbody2D rb2D;

    public GameManager managerGame;

    public GameObject DeathScreen,BannerAd;

    public AudioClip Point;

    public AudioSource Sounds,DeadScreenSound;

    public Animator anim;

    public GameObject BGNight, BGMorning;

    public Text TimeText,DeadTimeText,DeadSceneHeal;

    private int BirdSDeger;

    private float volume;

    private void Start()
    {
        volume = PlayerPrefs.GetFloat("Volume");
        Sounds.volume = volume;
        DeadScreenSound.volume = volume;
        BirdSDeger = PlayerPrefs.GetInt("SkinDegeri");
        if (BirdSDeger == 1)
        {anim.Play("RedBird");}
        else if (BirdSDeger == 2)
        {anim.Play("BlueBird");}
        else
        {anim.Play("YellowBird");}
        Sounds.clip = Point;
        SaatDeger = DateTime.Now.Hour;
        if (SaatDeger >= 18 || SaatDeger <= 6)
        {BGNight.SetActive(true);}
        else
        {BGMorning.SetActive(true);}
        Time.timeScale = 1;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            rb2D.velocity = Vector2.up * velocity;
        }
        
    }
    private void FixedUpdate()
    {
        Times();
    }

    private void Times()
    {
        if (isDead == false)
        {
            Zaman += Time.deltaTime;
        }
        if (Zaman >= 60)
        {
            dakika++;
            Zaman = 0;
        }
        if (dakika == 0)
        {
            TimeText.text = "" + (int)Zaman;
        }
        else
        {
            TimeText.text = dakika + ":" + (int)Zaman;
        }
    }

    public void TimeSetButton()
    {
        Zaman = PlayerPrefs.GetInt("DevamZaman");
    }

    private void TimeSetting()
    {
       LastTime = (int)Zaman;
       DeadTimeText.text = "" + (int)Zaman;
       if (Zaman>LastTime)
       {
           PlayerPrefs.SetString("HighZaman",""+(int)Zaman);
       }
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
            DeathScreen.SetActive(true);
            BannerAd.SetActive(true);
        }
    }

}