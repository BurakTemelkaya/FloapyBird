using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wait : MonoBehaviour
{
    public Text waitSeconds;

    public GameObject waitobje, Bird, Spawner;

    public GameObject BGNight, BGMorning;

    public Bird birdy;

    public Spawner spawn;
    void Start()
    {

        int SaatDeger = DateTime.Now.Hour;
        if (SaatDeger >= 18 || SaatDeger <= 6)
        { BGNight.SetActive(true); }
        else
        { BGMorning.SetActive(true); }

        StartCoroutine(WaitIE());

        waitobje.SetActive(true);

        Time.timeScale = 1;        
    }

    public void SetactiveFalse()
    {
        Bird.SetActive(false);

        Spawner.SetActive(false);

        waitobje.SetActive(true);
    }

    public IEnumerator WaitIE()
    {
        int s = 4;
        while (s!=0)
        {
            s--;

            waitSeconds.text = s.ToString();
            if (s==3)
            {
                waitSeconds.color = Color.red;
            }
            else if (s==2)
            {
                waitSeconds.color = Color.yellow;
            }
            else if(s==1)
            {
                waitSeconds.color = Color.green;                
            }
            else
            {
                Bird.SetActive(true);

                Spawner.SetActive(true);

                waitobje.SetActive(false);

                if (birdy.isDead)
                {
                    birdy.isDead = false;

                    StartCoroutine(birdy.IEZaman());

                    StartCoroutine(spawn.SpawnObject(spawn.time));
                }            
            }
            yield return new WaitForSeconds(1f);
        }
            
    }

    
}
