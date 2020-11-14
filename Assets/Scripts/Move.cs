using UnityEngine;

public class Move : MonoBehaviour
{

    public float speed,dstry,roz;

    private int Dif,r;

    private bool y, z;

    public Spawner spawn;

    public void Start()
    {
        random();
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

    public void random()
    {          
            r = Random.Range(0, 2);
            if (r == 0)
            {
                y = false;
                z = true;
            }
            else
            {
                y = true;
                z = false;
            }                       
    }

    void FixedUpdate()
    {
            transform.localPosition += Vector3.left * speed * Time.deltaTime;
            if (Dif >= 1)
            {
                Hard();
            }
            if (Dif == 2)
            {
                VeryHard();
            }            
    }
    private void Hard()
    {
        if (!y)
        {
            transform.localPosition += Vector3.up * Time.deltaTime * 0.25f;
            if (transform.localPosition.y >= 0.8f)
            {
                y = true;
            }
        }
        else
        {
            transform.localPosition -= Vector3.up * Time.deltaTime * 0.25f;
            if (transform.localPosition.y <= -0.4f)
            {
                y = false;
            }
        }
    }
    public void VeryHard()
    {
        if (z)
        {
            roz += Time.deltaTime*30;
            transform.localRotation = Quaternion.Euler(0, 0, roz);
            if (transform.localRotation.z >= 0.25f)
            {
                z = false;
            }
        }      
        else
        {
            roz -= Time.deltaTime*30;
            transform.localRotation = Quaternion.Euler(0, 0, roz);
            if (transform.localRotation.z <= -0.25f)
            {           
                z = true;
            }
        }
    }
}
