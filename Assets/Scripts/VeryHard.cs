using UnityEngine;

public class VeryHard : MonoBehaviour
{
    bool y,z;
    float roz;

    private void Start()
    {
        int r = Random.Range(0, 2);
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
    void Update()
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
        if (z)
        {
            roz += Time.deltaTime * 30;
            transform.localRotation = Quaternion.Euler(0, 0, roz);
            if (transform.localRotation.z >= 0.25f)
            {
                z = false;
            }
        }
        else
        {
            roz -= Time.deltaTime * 30;
            transform.localRotation = Quaternion.Euler(0, 0, roz);
            if (transform.localRotation.z <= -0.25f)
            {
                z = true;
            }
        }
    }
}
