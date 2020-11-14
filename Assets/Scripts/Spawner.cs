using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Bird BirdScript;

    public GameObject Boru;

    public float height;

    private float RandomHeight;

    public float time;

    private int dif;

    public Move mv;
    private void Start()
    {
        dif = PlayerPrefs.GetInt("Dif");
        if (dif==2)
        {
            time = 2.5f;
        }
        StartCoroutine(SpawnObject(time));
    }
    public IEnumerator SpawnObject(float time)
    {       
        while (!BirdScript.isDead)
        {

            RandomHeight =Random.Range(-height, height+0.2f);

            Instantiate(Boru, new Vector3(1, RandomHeight, 0), Quaternion.identity);

            mv.random();

            yield return new WaitForSeconds(time);
        }      
    }
}
