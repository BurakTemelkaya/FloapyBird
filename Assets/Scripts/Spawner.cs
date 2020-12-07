using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Bird BirdScript;

    public GameObject Boru,Normal,Hard,VeryHard;

    public float height;

    private float RandomHeight;

    public float time;

    private int dif;

    public Move mv;
    private void Start()
    {
        dif = PlayerPrefs.GetInt("Dif");
        if (dif==0)
        {
            Boru = Normal;
        }
        if (dif==1)
        {
            Boru = Hard;
        }
        if (dif==2)
        {
            time = 2.5f;
            Boru = VeryHard;
        }
        StartCoroutine(SpawnObject(time));
    }
    public IEnumerator SpawnObject(float time)
    {       
        while (!BirdScript.isDead)
        {
            RandomHeight =Random.Range(-height, height+0.2f);

            Instantiate(Boru, new Vector2(1, RandomHeight), Quaternion.identity);

            yield return new WaitForSeconds(time);
        }      
    }
}
