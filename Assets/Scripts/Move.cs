using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed,dstry,roz;

    private int Dif;

    public Spawner spawn;

    public void Start()
    {
        Dif = PlayerPrefs.GetInt("Dif");
        if (Dif==2)
        {
            dstry = 3.5f;
        }
        else
        {
            dstry = 3.1f;
        }
        Destroy(gameObject, dstry);       
    }
    void Update()
    {
        transform.localPosition += Vector3.left * speed * Time.deltaTime;         
    }
}
