﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class BGColor : MonoBehaviour
{
    private byte r=255, g=0, b=0;

    public Image BG;

    public Text HScore, HTime,TScore,THTime,HealText,Heal,Title,Dif,DifLabel;

    public int Color;
    void Start()
    {
        Time.timeScale = 1;
        
    }

    void Update()
    {
            
            if (r >= 255 && g <= 255 && b <= 0 )
            {
                g += Convert.ToByte(Time.deltaTime * Color);
            }

             if (g >= 255 && r >= 0 && b <= 0)
            {
                r -= Convert.ToByte(Time.deltaTime * Color);
            }

             if (g >= 255 && b <= 255 && r <= 0)
            {
                b += Convert.ToByte(Time.deltaTime * Color);
            }

             if (b >= 255 && g >= 0 && r <= 0)
            {
                g -= Convert.ToByte(Time.deltaTime * Color);
            }

             if (b >= 255 && r <= 255 && g <= 0)
            {
                r += Convert.ToByte(Time.deltaTime * Color);
            }

             if (r >= 255 && b >= 0 && g <= 0)
            {
                b -= Convert.ToByte(Time.deltaTime * Color);
            }
             

        BG.color = new Color32(r, g, b, 255);

            Title.color = new Color32(g, b, r, 255);

            HTime.color= new Color32(b, r, g, 255);

            HScore.color = new Color32(b, r, g, 255);

            THTime.color = new Color32(b, r, g, 255);
            TScore.color= new Color32(b, r, g, 255);

            HealText.color = new Color32(b, r, g, 255);
            Heal.color = new Color32(b, r, g, 255);

            Dif.color = new Color32(b, r, g, 255);
            DifLabel.color = new Color32(b, r, g, 255);

    }
}